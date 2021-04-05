using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace MessageClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAdress = host.AddressList[0];
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAdress, 1669);

            Socket clientSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            clientSocket.Connect(remoteEndPoint);
            MessageBox.Show($"Socket connected to {clientSocket.RemoteEndPoint.ToString()}");

            byte[] message = Encoding.ASCII.GetBytes("This is a test");

            int messageSent = clientSocket.Send(message);
        }
    }
}
