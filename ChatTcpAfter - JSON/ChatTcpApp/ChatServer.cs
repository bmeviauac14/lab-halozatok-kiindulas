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
using Newtonsoft.Json;
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
                listener = new TcpListener(port);
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
                //BinaryFormatter formatter = new BinaryFormatter();
                //(Message)formatter.Deserialize(netStream);
                string line = new StreamReader(netStream).ReadLine();
                Message message = JsonConvert.DeserializeObject<Message>(line);
                addMessageToTextBoxSafe(tbHistory, message, tcpClient.Client);
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

        private delegate void AddMessageToTextBoxSafe(TextBox tbMessages, Message message, Socket client);
        private void addMessageToTextBoxSafe(TextBox tbMessages, Message message, Socket client)
        {
            if (tbMessages.InvokeRequired)
            {
                tbMessages.Invoke(new AddMessageToTextBoxSafe(addMessageToTextBoxSafe),
                    tbMessages, message, client);
            }
            else
            {
                tbMessages.Text += String.Format("\r\n{0} ({1}): {2}", 
                    message.Sender, client.RemoteEndPoint, message.Text);
                tbMessages.SelectionStart = tbMessages.Text.Length;
                tbMessages.ScrollToCaret();
            }
        }
    }
}
