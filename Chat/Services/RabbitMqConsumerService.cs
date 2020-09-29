using Chat.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services
{
    // RabbitMQService.cs
    public class RabbitMqConsumerService : IRabbitMqConsumerService
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;

        protected readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMqConsumerService> _logger;
        private readonly IConfiguration _config;

        public RabbitMqConsumerService(IServiceProvider serviceProvider, ILogger<RabbitMqConsumerService> logger, IConfiguration config)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _config = config;

            var hostName = _config.GetValue<String>("RabbitMQHostname");
            var port = _config.GetValue<int>("RabbitMQPort");
            // Opens the connections to RabbitMQ
            _factory = new ConnectionFactory() { HostName = hostName, Port=port };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

         
        }

        public virtual void Connect()
        {
            var queueName = _config.GetValue<String>("QueueAnswersName");
            try
            {
                _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
            }
            catch
            {
                _logger.LogError("Queue already exists. Please choose another name or delete the queue");
                throw;
            }
            // Declare a RabbitMQ Queue


            var consumer = new EventingBasicConsumer(_channel);

            // Consume a RabbitMQ Queue
            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            // When we receive a message from rabbitmq

            consumer.Received += delegate (object model, BasicDeliverEventArgs ea)
            {
                // Get the ChatHub from SignalR (using DI)
                var chatHub = (IHubContext<ChatHub>)_serviceProvider.GetService(typeof(IHubContext<ChatHub>));



                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                chatHub.Clients.All.SendAsync("ReceiveMessage", "BOT", message);

            };


        }

    }

}
