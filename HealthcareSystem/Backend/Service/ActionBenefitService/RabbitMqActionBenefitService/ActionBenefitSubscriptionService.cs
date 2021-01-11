using Backend.Communication.RabbitMqConnection;
using RabbitMQ.Client;
using System;

namespace Backend.Service
{
    public class ActionBenefitSubscriptionService : IRabbitMqActionBenefitService
    {
        private readonly IModel _channel;
        private readonly String _queueName;

        public ActionBenefitSubscriptionService(IRabbitMqConnection connection)
        {
            if(connection.Connection != null)
                _channel = connection.Connection.CreateModel();
            _queueName = connection.Configuration.ActionBenefitQueueName;
        }

        ~ActionBenefitSubscriptionService()
        {
            if (_channel != null)
                _channel.Dispose();
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
    }
}
