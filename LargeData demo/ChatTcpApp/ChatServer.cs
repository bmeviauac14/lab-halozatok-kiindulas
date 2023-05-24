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
                // The Stream class has no notion of a "message". The Read method blocks until
                // more data becomes available if none is already in the buffer. It will
                // however return 0 when no more data can be received, which in this case
                // means the connection is closed.
                tcpClient = (TcpClient)client;

                // Egyiknek sincs hatása
                // tcpClient.ReceiveTimeout = 5000;
                // tcpClient.Client.ReceiveTimeout = 5000;
                // netStream.ReadTimeout = 5000;

                netStream = tcpClient.GetStream();
                
                //BinaryFormatter formatter = new BinaryFormatter();
                //Message message = (Message)formatter.Deserialize(netStream);
                byte[] buff = new byte[2000000];
                int lengtOfDataReceived = netStream.Read(buff, 0, 4);
                if (lengtOfDataReceived != 4)
                    return;
                int lenght = BitConverter.ToInt32(buff, 0);
                lengtOfDataReceived = netStream.Read(buff, 0, lenght);

                // A következő sorra tegyünk töréspontot, nézzük meg, mennyi adat érkezett (lengtOfDataReceived)
                // Jó eséllyel nem érkezik meg egy lépésben az összes: vagyis jelen megoldásunk nem jó. 
                // Helyett egy while ciklusban kellene dolgozni: addig olvasni és összefűzni az adatokat, míg 
                // az összes várt meg nem érkezik.
                if (lengtOfDataReceived != lenght)
                    return;

                string text = Encoding.UTF8.GetString(buff, 0, lengtOfDataReceived);

                string receivedText = text[0] + ".." + text[text.Length - 1] + ", " + text.Length;

                //addMessageToTextBoxSafe(tbHistory, message, tcpClient.Client);
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
