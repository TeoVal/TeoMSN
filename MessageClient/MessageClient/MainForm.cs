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

namespace MessageClient
{
    public partial class MainForm : Form
    {

        Socket clientSocket = null;
        ASCIIEncoding asciiEncoding = new ASCIIEncoding();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAdress = host.AddressList[0];
                IPEndPoint remoteEndPoint = new IPEndPoint(ipAdress, 1669);

                clientSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(remoteEndPoint);

                //MessageBox.Show($"Socket connected to {clientSocket.RemoteEndPoint.ToString()}");
                if (CheckIfConnected())
                {
                    MessageBox.Show($"Socket connected.");
                    connectionStatus.Text = "Connected to server";
                }
                else
                {
                    connectionStatus.Text = "Not connected to server.";
                }


            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine("Socket error. Client could not connect to the server.");
                connectionStatus.Text = "Not connected to server.";
            }
            
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            byte[] sendingMessage = new byte[1500];
            sendingMessage = asciiEncoding.GetBytes(messageBox.Text);
            clientSocket.Send(sendingMessage);

            listChat.Items.Add("Me:    " + messageBox.Text);
            messageBox.Text = "";
        }

        private bool CheckIfConnected()
        {
            bool isConnected = false;
            Ping ping = new Ping();
            string ip = "127.0.0.1";
            IPAddress address = IPAddress.Parse(ip);
            PingReply pong = ping.Send(address);
            if (pong.Status == IPStatus.Success)
            {
                Console.WriteLine(ip + " is up and running.");
                isConnected = true;
            }

            return isConnected;
        }
    }
}
