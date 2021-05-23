using System;
using System.Collections.Generic;
using System.Text;

namespace MessageClient
{
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs args);

    public class MessageReceivedEventArgs : EventArgs
    {
        public Message Data { get; private set; }

        public MessageReceivedEventArgs(Message data)
        {
            this.Data = data;
        }
    }
}
