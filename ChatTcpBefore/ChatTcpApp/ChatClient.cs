using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Threading;

namespace ChatTcpApp
{
    public class ChatClient
    {
        private TextBox tbHistory;
        private string remoteHost;
        private string myName;
        private string text;

        public ChatClient(string remoteHost, string myName, string text, TextBox tbHistory)
        {
            this.remoteHost = remoteHost;
            this.myName = myName;
            this.text = text;
            this.tbHistory = tbHistory;
        }

        public void SendMessageAsync()
        {
            //  TODO: Send a message to the partner in a new thread
        }

        private void addMyMessageToTextBoxSafe(TextBox tbMessages, string text)
        {
            tbMessages.Text += String.Format("\r\n[Me]: {0}", text);
            tbMessages.SelectionStart = tbMessages.Text.Length;
            tbMessages.ScrollToCaret();
        }
    }
}
