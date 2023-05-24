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
            //  TODO: set the default 'My name' value to the IP address of the local computer
            
            //  TODO: start listening for connecting clients
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            //  TODO: send the message
        }
    }
}
