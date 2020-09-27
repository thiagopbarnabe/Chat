using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ChatHub(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendMessage(string user, string message)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(message.StartsWith('/'))
            {
                var messageCommand = message.Split('/');
                
                if(messageCommand.Length != 2)
                {
                    throw new System.Exception("Wrong message format");
                }

                var messageParts = messageCommand[1].Split('=');

                if(messageParts[0]=="stock")
                {
                    if (messageParts.Length != 2)
                    {
                        throw new System.Exception("Wrong command format");
                    }

                    
                        
                }

                
                
                
            }
            
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
        }
    }
}
