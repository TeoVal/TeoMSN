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

        private string userName;

        public MainForm()
        {
            InitializeComponent();
            messageServerConnector = new MessageServerConnector();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AskUserName();
            
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

        private void AskUserName()
        {
            string answer = QuestionBox.Ask("Whats's your name", "User name");

            if (answer == null)
            {
                MessageBox.Show("You need to enter a name.The application will close");
                this.Close();
            }
            else
            {
                userName = answer;
            }
        }

        public void HandleMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            AddMesssageToList(args.Data);
        }

        private void AddMesssageToList(Message msg)
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
                listChat.Items.Add($"{msg.Sender}: " + msg.Content);
            }
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            try 
            {
                Message message = new Message(messageBox.Text, userName);
                messageServerConnector.SendMessage(message);
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
