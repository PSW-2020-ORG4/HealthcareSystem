using Backend.Service;
using RabbitMQ.Client;

namespace Backend.Communication.RabbitMqConnection
{
    public interface IRabbitMqConnection
    {
        RabbitMqConfiguration Configuration { get; }
        IConnection Connection { get; } 
    }
}
