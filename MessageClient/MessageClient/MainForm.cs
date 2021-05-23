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
using MessageClient.Utils;

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
            string answer = QuestionBox.Ask("Whats's your name", "User name");
            
            messageServerConnector.Connect(connectionStatus.Text);

            messageServerConnector.MessageReceived += HandleMessageReceived;

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

        public void HandleMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            AddMesssageToList(args.Data);
        }

        private void AddMesssageToList(string msg)
        {
            if (base.InvokeRequired)
            {
                base.Invoke(new MethodInvoker(() =>
                {
                    AddMesssageToList(msg);
                }));
            }
            else
            {
                listChat.Items.Add("Me:    " + msg);
            }
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            try 
            {
                messageServerConnector.SendMessage(messageBox.Text);
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
