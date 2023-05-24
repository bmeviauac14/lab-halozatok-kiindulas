using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChatTcpApp
{
    public class ChatServer
    {
        private TextBox tbHistory;

        public ChatServer(TextBox tbHistory)
        {
            this.tbHistory = tbHistory;
        }

        public void Start()
        {
            //  TODO: Start listening a port in a new thread
            //      All the client requests should be processed in a separate thread
        }

        private void addMessageToTextBoxSafe(TextBox tbMessages, Message message, Socket client)
        {
            tbMessages.Text += String.Format("\r\n{0} ({1}): {2}", 
                message.Sender, client.RemoteEndPoint, message.Text);
            tbMessages.SelectionStart = tbMessages.Text.Length;
            tbMessages.ScrollToCaret();
        }
    }
}
