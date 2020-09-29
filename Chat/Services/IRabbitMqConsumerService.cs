using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Services
{
    interface IRabbitMqConsumerService
    {
        void Connect();        
    }
}
