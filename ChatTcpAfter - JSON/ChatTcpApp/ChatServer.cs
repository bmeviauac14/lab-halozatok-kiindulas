using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Diagnostics;
using System.Text.Json;
using System.IO;

namespace ChatTcpApp
{
    public class ChatServer
    {
        private TcpListener listener;
        private TextBox tbHistory;

        public ChatServer(TextBox tbHistory)
        {
            this.tbHistory = tbHistory;
        }

        public void Start()
        {
            Thread thread = new Thread(ServerThreadFunc);
            thread.IsBackground = true;
            thread.Start();
        }

        private void ServerThreadFunc()
        {
            try
            {
                int port = int.Parse(ConfigurationManager.AppSettings["port"]);
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                Trace.TraceInformation("Server has been started...");
            }
            catch (SocketException ex)
            {
                Trace.TraceError(ex.Message);
                MessageBox.Show(ex.Message);
                return;
            }

            while (true)
            {
                try
                {
                    TcpClient newClient = listener.AcceptTcpClient();
                    Trace.TraceInformation("New incoming client request.");
                    ThreadPool.QueueUserWorkItem(handleClient, newClient);
                }
                catch (SocketException ex)
                {
                    Trace.TraceError(ex.Message);
                }
            }
        }

        private void handleClient(object client)
        {
            TcpClient tcpClient = null;
            NetworkStream netStream = null;
            try
            {
                tcpClient = (TcpClient)client;
                netStream = tcpClient.GetStream();
                //XmlSerializer serializer = new XmlSerializer(typeof(Message));
                //Message message = (Message)serializer.Deserialize(netStream);
                string line = new StreamReader(netStream).ReadLine();
                Message message = JsonSerializer.Deserialize<Message>(line);
                AddMessageToTextBoxSafe(tbHistory, message, tcpClient.Client);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
            finally
            {
                if (netStream != null)
                    netStream.Close();
                if (tcpClient != null)
                    tcpClient.Close();
            }
        }

        private void AddMessageToTextBoxSafe(TextBox tbMessages, Message message, Socket client)
        {
            if (tbMessages.InvokeRequired)
            {
                tbMessages.Invoke(AddMessageToTextBoxSafe, tbMessages, message, client);
            }
            else
            {
                tbMessages.Text += $"\r\n{message.Sender} ({client.RemoteEndPoint}): {message.Text}";
                tbMessages.SelectionStart = tbMessages.Text.Length;
                tbMessages.ScrollToCaret();
            }
        }
    }
}
