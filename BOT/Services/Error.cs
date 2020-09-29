using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOT.Services
{
    class Error:IError
    {
        private IModel _channelError;
        private string _queueErrorName;

        public Error(IModel channelError, string queueErrorName)
        {
            _queueErrorName = queueErrorName;
            _channelError = channelError;
        }

        
        public void AddError(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channelError.BasicPublish(exchange: "", routingKey: _queueErrorName, basicProperties: null, body: body);
            Console.WriteLine(" [x] ERROR Sent {0}", message);
        }
    }
}
