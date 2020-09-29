using System;
using System.Collections.Generic;
using System.Text;

namespace BOT
{
    public interface IMessageProcessing
    {
        void ProcessMessage(string message);
    }
}
