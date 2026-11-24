using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatTcpApp
{
    public class Message
    {
        public string Sender { get; set; }
        public string Text { get; set; }

        // Az XmlSerializer (és deszerializáláskor a System.Text.Json) paraméter nélküli konstruktort igényel.
        public Message()
        {
        }

        public Message(string sender, string text)
        {
            this.Sender = sender;
            this.Text = text;
        }
    }
}
