using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatTcpApp
{
    public class Message
    {
        public string Sender;
        public string Text;

        public Message(string sender, string text)
        {
            this.Sender = sender;
            this.Text = text;
        }
    }
}
