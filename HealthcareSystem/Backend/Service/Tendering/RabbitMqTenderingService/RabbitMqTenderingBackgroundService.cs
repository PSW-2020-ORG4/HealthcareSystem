using Backend.Communication.RabbitMqConnection;
using Backend.Model.Pharmacies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Service.Tendering.RabbitMqTenderingService
{
    public class RabbitMqTenderingBackgroundService : BackgroundService
    {
        private IModel _channel;
        private readonly IServiceProvider _service;
        private readonly String _exchangeName;

        public RabbitMqTenderingBackgroundService()
        {
        }

        public RabbitMqTenderingBackgroundService(IServiceProvider service, IRabbitMqConnection connection)
        {
            if (connection.Connection != null)
                _channel = connection.Connection.CreateModel();
            _exchangeName = connection.Configuration.TenderExchangeName;
            _service = service;
            InitializeRabbitMqListener();
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
                    _channel.QueueDeclare(queue: t.QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
                    _channel.QueueBind(queue: t.QueueName, exchange: _exchangeName, routingKey: t.RoutingKey);
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
                if (tender.IsClosed)
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested && _channel != null)
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += OnConsumerReceived;

                using (var scope = _service.CreateScope())
                {
                    ITenderService tenderService = scope.ServiceProvider.GetRequiredService<ITenderService>();

                    foreach (Tender t in tenderService.GetAllNotClosedTenders())
                    {
                        _channel.BasicConsume(queue: t.QueueName, autoAck: false, consumer: consumer);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
