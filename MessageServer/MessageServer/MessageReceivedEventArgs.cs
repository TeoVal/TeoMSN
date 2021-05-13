using System;
using System.Collections.Generic;
using System.Text;

namespace MessageServer
{
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

    public class MessageReceivedEventArgs: EventArgs
    {
        public string Data { get; private set; }

        public MessageReceivedEventArgs(string data)
        {
            this.Data = data;
        }
    }
}
