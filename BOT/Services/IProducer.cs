using RabbitMQ.Client;

namespace BOT
{
    public interface IProducer
    {   
        void Produce(string message);
    }
}