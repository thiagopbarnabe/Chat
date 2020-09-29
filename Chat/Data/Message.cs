using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Data
{
    public class Message
    {
        public int Id { get; set; }        
        public string UserName { get; set; }
        public string Body { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
