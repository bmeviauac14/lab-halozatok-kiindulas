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
using Newtonsoft.Json;
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

                //BinaryFormatter formatter = new BinaryFormatter();
                //formatter.Serialize(netStream, message);

                using (StreamWriter sw = new StreamWriter(netStream))
                {
                    // A JsonConvert.SerializeObject escape-eli a tartalomban levő sortöréseket.
                    string dataToSend = JsonConvert.SerializeObject(message);
                    sw.WriteLine(dataToSend);
                }

                Trace.TraceInformation("Message sent by client");
                addMyMessageToTextBoxSafe(tbHistory, message.Text);
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

        private delegate void AddMyMessageToTextBoxSafe(TextBox tbMessages, string text);
        private void addMyMessageToTextBoxSafe(TextBox tbMessages, string text)
        {
            if (tbMessages.InvokeRequired)
            {
                tbMessages.Invoke(new AddMyMessageToTextBoxSafe(addMyMessageToTextBoxSafe),
                    tbMessages, text);
            }
            else
            {
                tbMessages.Text += String.Format("\r\n[Me]: {0}", text);
                tbMessages.SelectionStart = tbMessages.Text.Length;
                tbMessages.ScrollToCaret();
            }
        }
    }
}
