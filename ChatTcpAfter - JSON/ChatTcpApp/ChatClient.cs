using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Text.Json;
using System.IO;

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
            ThreadPool.QueueUserWorkItem(SendMessage);
        }

        private void SendMessage(object state)
        {
            TcpClient client = null;
            NetworkStream netStream = null;
            try
            {
                int port = int.Parse(ConfigurationManager.AppSettings["port"]);
                client = new TcpClient();
                client.Connect(remoteHost, port);
                netStream = client.GetStream();
                Message message = new Message(myName, text);

                //XmlSerializer serializer = new XmlSerializer(typeof(Message));
                //serializer.Serialize(netStream, message);

                using (StreamWriter sw = new StreamWriter(netStream))
                {
                    // A JsonSerializer.Serialize escape-eli a tartalomban levő sortöréseket.
                    string dataToSend = JsonSerializer.Serialize(message);
                    sw.WriteLine(dataToSend);
                }

                Trace.TraceInformation("Message sent by client");
                AddMyMessageToTextBoxSafe(tbHistory, message.Text);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (netStream != null)
                    netStream.Close();
                if (client != null)
                    client.Close();
            }
        }

        private void AddMyMessageToTextBoxSafe(TextBox tbMessages, string text)
        {
            if (tbMessages.InvokeRequired)
            {
                tbMessages.Invoke(AddMyMessageToTextBoxSafe, tbMessages, text);
            }
            else
            {
                tbMessages.Text += $"\r\n[Me]: {text}";
                tbMessages.SelectionStart = tbMessages.Text.Length;
                tbMessages.ScrollToCaret();
            }
        }
    }
}
