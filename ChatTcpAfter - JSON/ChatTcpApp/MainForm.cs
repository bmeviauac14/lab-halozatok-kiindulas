using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace ChatTcpApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string host = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(host);
            foreach (IPAddress address in hostEntry.AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    tbMyName.Text = address.ToString();
                    break;
                }
            }
            ChatServer chatServer = new ChatServer(tbHistory);
            chatServer.Start();
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            ChatClient chatClient = new ChatClient(tbPartner.Text, tbMyName.Text, 
                tbMessageToSend.Text, tbHistory);
            chatClient.SendMessageAsync();
        }
    }
}
