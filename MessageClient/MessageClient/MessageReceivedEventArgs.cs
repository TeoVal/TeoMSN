﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MessageClient
{
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs args);

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Data { get; private set; }

        public MessageReceivedEventArgs(string data)
        {
            this.Data = data;
        }
    }
}
