using Chat.Data;
using Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        private readonly IRabbitMqProducerService _producer;

        public ChatHub(IHttpContextAccessor httpContextAccessor, Chat.Data.ApplicationDbContext dbContext, IRabbitMqProducerService producer) 
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _producer = producer;
        }

        public async Task SendMessage(string message)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            if(message.StartsWith('/'))
            {
                var messageCommand = message.Split('/');
                
                if(messageCommand.Length != 2)
                {
                    throw new System.Exception("Wrong message format");
                }

                await Clients.All.SendAsync("ReceiveMessage", userName, message);
                _producer.Produce(messageCommand[1]);
                return;
            }

            _dbContext.Messages.Add(new Message
            {   
                Body = message,
                UserName = userName,
                TimeStamp = DateTime.Now
            });
            _dbContext.SaveChanges();

            await Clients.All.SendAsync("ReceiveMessage", userName, message);
        }
    }
}
