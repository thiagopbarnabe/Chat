namespace Chat.Services
{
    public interface IRabbitMqProducerService
    {
        void Produce(string message);
    }
}