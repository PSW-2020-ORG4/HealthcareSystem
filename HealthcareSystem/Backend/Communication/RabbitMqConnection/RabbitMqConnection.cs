using Backend.Communication.RabbitMqConfuguration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading;

namespace Backend.Communication.RabbitMqConnection
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        public RabbitMqConfiguration Configuration { get; private set; }
        public IConnection Connection { get; private set; }

        public RabbitMqConnection(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            Configuration = rabbitMqOptions.Value;
            InitializeConnection();

        }

        private void InitializeConnection()
        {
            Console.WriteLine($"Host: {Configuration.Host}, VHost: {Configuration.VHost}, User: {Configuration.Username}");
            Console.WriteLine($"Retry: {Configuration.RetryCount}, Retry wait: {Configuration.RetryWait}");
            for (int i = 0; i <= Configuration.RetryCount; i++)
            {
                try
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = Configuration.Host,
                        VirtualHost = Configuration.VHost,
                        UserName = Configuration.Username,
                        Password = Configuration.Password
                    };
                    Connection = factory.CreateConnection();
                    Connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                    Console.WriteLine("RabbitMq connection established.");
                    break;
                }
                catch (BrokerUnreachableException)
                {
                    if (i < Configuration.RetryCount)
                    {
                        Console.WriteLine("RabbitMq connection failed. Retrying.");
                        Thread.Sleep(Configuration.RetryWait);
                    }
                    else
                    {
                        Console.WriteLine("RabbitMq connection failed. Disposing.");
                        CloseConnection();
                    }
                }
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            CloseConnection();
        }

        private void CloseConnection()
        {
            if (Connection != null)
                Connection.Dispose();
        }
    }
}
