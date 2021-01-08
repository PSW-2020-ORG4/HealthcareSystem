using Backend.Communication.RabbitMqConnection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Backend.Service.Tendering.RabbitMqTenderingService
{
    public class RabbitMqTenderingBackgroundService : BackgroundService
    {
        private IModel _channel;
        private readonly String _queueName;

        public RabbitMqTenderingBackgroundService()
        {
        }

        public RabbitMqTenderingBackgroundService(IRabbitMqConnection connection)
        {
            if (connection.Connection != null)
                _channel = connection.Connection.CreateModel();
            _queueName = connection.Configuration.TenderQueueName;
        }



        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
