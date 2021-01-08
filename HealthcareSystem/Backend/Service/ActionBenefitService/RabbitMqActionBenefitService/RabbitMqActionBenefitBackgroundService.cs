using Backend.Communication.RabbitMqConnection;
using Backend.Model;
using Backend.Model.Pharmacies;
using Backend.Service.Pharmacies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class RabbitMqActionBenefitMessageingService : BackgroundService
    {
        private IModel _channel;
        private readonly IServiceProvider _service;
        private readonly String _queueName;

        public RabbitMqActionBenefitMessageingService()
        {
        }

        public RabbitMqActionBenefitMessageingService(IServiceProvider service, IRabbitMqConnection connection)
        {
            _service = service;
            if(connection.Connection != null)
                _channel = connection.Connection.CreateModel();
            _queueName = connection.Configuration.ActionBenefitQueueName;
            InitializeRabbitMqListener();
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested && _channel != null)
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += OnConsumerReceived;

                _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            }
            return Task.CompletedTask;
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
                body = e.Body.ToArray();
                messageJson = Encoding.UTF8.GetString(body);
                message = JsonConvert.DeserializeObject<ActionBenefitMessage>(messageJson);
                exchangeName = e.Exchange;
                HandleMessage(message, exchangeName);
                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _channel.BasicReject(e.DeliveryTag, false);
            }
        }

        public override void Dispose()
        {
            if (_channel != null)
                _channel.Dispose();

            base.Dispose();
        }
    }
}