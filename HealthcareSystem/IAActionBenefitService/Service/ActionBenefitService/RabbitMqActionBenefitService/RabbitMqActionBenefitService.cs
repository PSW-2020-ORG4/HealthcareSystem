using Backend.Communication.RabbitMqConnection;
using Backend.Model;
using Backend.Model.Pharmacies;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace IntegrationAdaptersActionBenefitService.Service
{
    public class RabbitMqActionBenefitService : IRabbitMqActionBenefitService
    {
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        private readonly IServiceProvider _service;
        private readonly String _queueName;
        private readonly MailSettings _mailSettings;

        public RabbitMqActionBenefitService(IServiceProvider service, IRabbitMqConnection connection, IOptions<MailSettings> mailSettings)
        {
            if (connection.Connection != null)
            {
                _channel = connection.Connection.CreateModel();
                _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += OnConsumerReceived;
            }
            _queueName = connection.Configuration.ActionBenefitQueueName;
            _service = service;
            _mailSettings = mailSettings.Value;
            InitializeRabbitMqListener();
        }

        ~RabbitMqActionBenefitService()
        {
            if (_channel != null)
                _channel.Dispose();
        }

        private void InitializeRabbitMqListener()
        {
            if (_channel == null)
                return;
            _channel.QueueDeclare(queue: _queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: _consumer);
            using (var scope = _service.CreateScope())
            {
                IPharmacyService pharmacyService = scope.ServiceProvider.GetRequiredService<IPharmacyService>();

                foreach (PharmacySystem p in pharmacyService.GetPharmaciesBySubscribed(true))
                {
                    _channel.ExchangeDeclare(exchange: p.ActionsBenefitsExchangeName, type: ExchangeType.Fanout);
                    _channel.QueueBind(queue: _queueName, exchange: p.ActionsBenefitsExchangeName, routingKey: "");
                }
            }

        }

        private void HandleMessage(ActionBenefitMessage message, string exchangeName)
        {
            using (var scope = _service.CreateScope())
            {
                IActionBenefitService actionBenefitService = scope.ServiceProvider.GetRequiredService<IActionBenefitService>();

                actionBenefitService.CreateActionBenefit(exchangeName, message);
            }
        }

        private void OnConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            Byte[] body = null;
            string messageJson = null;
            ActionBenefitMessage message = null;
            string exchangeName = null;
            try
            {
                exchangeName = e.Exchange;
                body = e.Body.ToArray();
                messageJson = Encoding.UTF8.GetString(body);
                message = JsonConvert.DeserializeObject<ActionBenefitMessage>(messageJson);
                HandleMessage(message, exchangeName);
                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _channel.BasicReject(e.DeliveryTag, false);
                using (var scope = _service.CreateScope())
                {
                    IPharmacyService pharmacyService = scope.ServiceProvider.GetRequiredService<IPharmacyService>();
                    PharmacySystem pharmacySystem = pharmacyService.GetPharmacyByExchangeName(exchangeName);
                    if (pharmacySystem != null)
                        SendEmail(ex.Message, pharmacySystem.Email);
                }
            }
        }

        public void Subscribe(string exchangeName)
        {
            if (_channel == null)
                return;

            if (exchangeName == null || exchangeName.Trim() == "")
                throw new ArgumentException("Non valid exchange name");

            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
            _channel.QueueBind(queue: _queueName, exchange: exchangeName, routingKey: "");
        }

        public void Unsubscribe(string exchangeName)
        {
            if (_channel == null)
                return;

            if (exchangeName == null || exchangeName.Trim() == "")
                throw new ArgumentException("Non valid exchange name");

            _channel.QueueUnbind(queue: _queueName, exchange: exchangeName, "", null);
        }

        public void SubscriptionEdit(string exOld, bool subOld, string exNew, bool subNew)
        {
            if (_channel == null)
                return;

            if (subOld != subNew || exOld != exNew)
            {
                if (subOld && !subNew && exOld == exNew)
                    Unsubscribe(exOld);
                else if (!subOld && subNew && exOld == exNew)
                    Subscribe(exNew);
                else if (subOld && subNew && exOld != exNew)
                {
                    Unsubscribe(exOld);
                    Subscribe(exNew);
                }
                else if (!subOld && subNew)
                    Subscribe(exNew);
            }
        }

        private void SendEmail(string content, string recipient)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = "ActionBenefit message error!";
            var builder = new BodyBuilder();
            builder.HtmlBody =
                "ActionBenefit message should be Json String with Subject and Message fields, type string, neither can be null or empty string! <br>" + content;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}