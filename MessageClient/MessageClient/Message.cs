using System;
using System.Collections.Generic;
using System.Text;

namespace MessageClient
{
    public class Message
    {
        public string Content { get; private set; }
        public string Sender { get; set; }

        public Message(string content, string sender)
        {
            this.Content = content;
            this.Sender = sender;
        }
    }
}
