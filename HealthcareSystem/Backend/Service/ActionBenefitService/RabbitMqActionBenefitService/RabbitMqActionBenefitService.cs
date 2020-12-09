using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Model.Pharmacies;
using Backend.Service.Pharmacies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Service
{
    public class RabbitMqActionBenefitMessageingService : BackgroundService, IActionBenefitMessageingService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IServiceProvider _service;
        private readonly String _hostname;
        private readonly String _username;
        private readonly String _password;
        private readonly String _queueName;

        public RabbitMqActionBenefitMessageingService()
        {
        }

        public RabbitMqActionBenefitMessageingService(IServiceProvider service, IOptions<RabbitMqConfiguration> rabbitMqOPtions)
        {
            _service = service;
            _hostname = rabbitMqOPtions.Value.Hostname;
            _username = rabbitMqOPtions.Value.Username;
            _password = rabbitMqOPtions.Value.Password;
            _queueName = "bolnica-1";
            InitializeRabbitMqListener(0);
        }

        private void InitializeRabbitMqListener(int iteration)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };
                _connection = factory.CreateConnection();
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                _channel = _connection.CreateModel();
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
            catch (BrokerUnreachableException bue)
            {
                if (iteration < 3)
                {
                    //set wait time
                    Thread.Sleep(5000);
                    InitializeRabbitMqListener(++iteration);
                }
                else
                {
                    Console.WriteLine(bue.Message);
                    Dispose();
                }
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += OnConsumerReceived;
                consumer.Shutdown += OnConsumerShutdown;
                consumer.Registered += OnConsumerRegistered;
                consumer.Unregistered += OnConsumerUnregistered;
                consumer.ConsumerCancelled += OnConsumerCancelled;

                _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            }
            return Task.CompletedTask;
        }

        public void Subscribe(string exchangeName)
        {
            if (exchangeName == null || exchangeName.Trim() == "")
                throw new ArgumentException("Non valid exchange name");

            _channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);
            _channel.QueueBind(queue: _queueName, exchange: exchangeName, routingKey: "");
        }

        public void Unsubscribe(String exchangeName)
        {
            if (exchangeName == null || exchangeName.Trim() == "")
                throw new ArgumentException("Non valid exchange name");

            _channel.QueueUnbind(queue: _queueName, exchange: exchangeName, "", null);
        }

        private void HandleMessage(ActionBenefitMessage message, string exchangeName)
        {
            using (var scope = _service.CreateScope())
            {
                IActionBenefitService actionBenefitService = scope.ServiceProvider.GetRequiredService<IActionBenefitService>();

                actionBenefitService.CreateActionBenefit(exchangeName, message);
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Dispose();
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
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                _channel.BasicReject(e.DeliveryTag, false);
            }
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public override void Dispose()
        {
            if (_channel != null)
                _channel.Dispose();

            if (_connection != null)
                _connection.Dispose();

            base.Dispose();
        }

        public void SubscriptionEdit(string exOld, bool subOld, string exNew, bool subNew)
        {
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
    }
}