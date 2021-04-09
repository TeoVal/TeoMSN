using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageClient
{
    public partial class MainForm : Form
    {

        Socket clientSocket = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAdress = host.AddressList[0];
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAdress, 1669);

            clientSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect(remoteEndPoint);
            MessageBox.Show($"Socket connected to {clientSocket.RemoteEndPoint.ToString()}");
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            ASCIIEncoding asciiEncoding = new ASCIIEncoding();
            byte[] sendingMessage = new byte[1500];
            sendingMessage = asciiEncoding.GetBytes(messageBox.Text);
            clientSocket.Send(sendingMessage);
        }
    }
}
