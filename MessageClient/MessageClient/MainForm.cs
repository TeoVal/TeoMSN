using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace MessageClient
{
    public partial class MainForm : Form
    {
        private MessageServerConnector messageServerConnector = null;

        public MainForm()
        {
            InitializeComponent();
            messageServerConnector = new MessageServerConnector();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            messageServerConnector.Connect(connectionStatus.Text);

            if (!messageServerConnector.IsConnected())
            {
                MessageBox.Show($"Socket is not connected.");
                connectionStatus.Text = "Not connected to server.";
            }
            else
            {
                connectionStatus.Text = "Connected to server.";
            }
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            try 
            {
                messageServerConnector.SendMessage(messageBox.Text);

                listChat.Items.Add("Me:    " + messageBox.Text);
                messageBox.Text = "";
            }
            catch(SocketException ex)
            {
                listChat.Items.Add("#######Message was not sent.Server is not connected to the client.#######");
                connectionStatus.Text = "Not connected to server.";
                messageBox.Text = "";
            }
        }
    }
}
