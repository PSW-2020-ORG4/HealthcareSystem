using System;
using System.Collections.Generic;
using System.Text;
using Backend.Communication.RabbitMqConnection;
using Backend.Model.Pharmacies;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace IntegrationAdaptersService4.Service.RabbitMqTenderingService
{
    public class RabbitMqTenderingService : IRabbitMqTenderingService
    {
        private readonly IModel _channel;
        private readonly EventingBasicConsumer _consumer;
        private readonly IServiceProvider _service;
        private readonly String _exchangeName;

        public RabbitMqTenderingService(IServiceProvider service, IRabbitMqConnection connection)
        {
            if (connection.Connection != null)
            {
                _channel = connection.Connection.CreateModel();
                _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += OnConsumerReceived;
            }
            _exchangeName = connection.Configuration.TenderExchangeName;
            _service = service;
            InitializeRabbitMqListener();
        }

        ~RabbitMqTenderingService()
        {
            if (_channel != null)
                _channel.Dispose();
        }

        private void InitializeRabbitMqListener()
        {
            if (_channel == null)
                return;
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
            using (var scope = _service.CreateScope())
            {
                ITenderService  tenderService = scope.ServiceProvider.GetRequiredService<ITenderService>();

                foreach (Tender t in tenderService.GetAllNotClosedTenders())
                {
                    if(t.EndDate.CompareTo(DateTime.Now) > 0)
                    {
                        _channel.QueueDeclare(queue: t.QueueName,
                                                          durable: true,
                                                          exclusive: false,
                                                          autoDelete: false,
                                                          arguments: null);
                        _channel.QueueBind(queue: t.QueueName, exchange: _exchangeName, routingKey: t.RoutingKey);
                        _channel.BasicConsume(queue: t.QueueName, autoAck: false, consumer: _consumer);
                    }
                    else
                    {
                        RemoveQueue(t);
                    }
                }
            }
        }

        private void OnConsumerReceived(object sender, BasicDeliverEventArgs e)
        {
            Byte[] body = null;
            string messageJson = null;
            TenderMessageDTO message = null;
            string routingKey = null;
            try
            {
                body = e.Body.ToArray();
                messageJson = Encoding.UTF8.GetString(body);
                message = JsonConvert.DeserializeObject<TenderMessageDTO>(messageJson);
                routingKey = e.RoutingKey;
                HandleMessage(message, routingKey);
                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _channel.BasicReject(e.DeliveryTag, false);
            }
        }

        private void HandleMessage(TenderMessageDTO message, string routingKey)
        {
            using (var scope = _service.CreateScope())
            {
                ITenderService tenderService = scope.ServiceProvider.GetRequiredService<ITenderService>();
                Tender tender = tenderService.GetTenderByRoutingKey(routingKey);
                if (tender.IsClosed || tender.EndDate.CompareTo(DateTime.Now) <= 0)
                    throw new Exception("Tender is closed!");

                TenderMessage tenderMessage = new TenderMessage();
                tenderMessage.Identification = message.Identification;
                tenderMessage.ReplyRoutingKey = message.ReplyRoutingKey;
                tenderMessage.TenderId = tender.Id;
                tenderMessage.Offers = new List<TenderOffer>();

                foreach(var o in message.Offers)
                {
                    TenderOffer to = new TenderOffer() {Code = o.Code, Name = o.Name, Quantity = o.Quantity, Price = o.Price };
                    tenderMessage.Offers.Add(to);
                }
                scope.ServiceProvider.GetRequiredService<ITenderMessageService>().CreateTenderMessage(tenderMessage);
            }
        }

        public void AddQueue(Tender tender)
        {
            if (_channel == null)
                return;

            _channel.QueueDeclare(queue: tender.QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
            _channel.QueueBind(queue: tender.QueueName, exchange: _exchangeName, routingKey: tender.RoutingKey);
            _channel.BasicConsume(queue: tender.QueueName, autoAck: false, consumer: _consumer);
        }

        public void RemoveQueue(Tender tender)
        {
            if (_channel == null)
                return;

            _channel.QueueUnbind(queue: tender.QueueName, exchange: _exchangeName, routingKey: tender.RoutingKey);
            _channel.QueueDelete(queue: tender.QueueName);
        }

        public void NotifyParticipants(int tenderId)
        {
            if (_channel == null)
                return;

            List<TenderMessage> messages = new List<TenderMessage>();
            using (var scope = _service.CreateScope())
            {
                messages = scope.ServiceProvider.GetRequiredService<ITenderMessageService>().GetAllByTender(tenderId);
                foreach (var tm in messages)
                {
                    string reply;
                    if (tm.IsAccepted)
                        reply = "Your offer won tender: " + tm.Tender.Name + "!";
                    else
                        reply = "Your offer did not win tender: " + tm.Tender.Name + "!";

                    var body = Encoding.UTF8.GetBytes(reply);
                    _channel.BasicPublish(exchange: _exchangeName,
                                          routingKey: tm.ReplyRoutingKey,
                                          basicProperties: null,
                                          body: body);

                }
            }
        }
    }
}
