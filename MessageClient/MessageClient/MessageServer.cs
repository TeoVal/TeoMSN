using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MessageClient
{
    class MessageServerConnector
    {
        private Socket socket = null;
        private IPEndPoint remoteEndPoint = null;
        private ASCIIEncoding asciiEncoding = new ASCIIEncoding();

        public void SendMessage(string messageBoxText)
        {
            byte[] sendingMessage = new byte[1500];
            sendingMessage = asciiEncoding.GetBytes(messageBoxText);
            socket.Send(sendingMessage);
        }

        public bool IsConnected()
        {
            // The short version:
            //return !((socket.Poll(1000, SelectMode.SelectRead) && (socket.Available == 0)) || !socket.Connected);

            bool pollStatus = socket.Poll(1000, SelectMode.SelectRead);
            bool socketStatus = (socket.Available == 0);
            if ((pollStatus && socketStatus) || !socket.Connected)
                return false;
            else
                return true;
        }

        public void Connect(string connectionStatus)
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAdress = host.AddressList[0];
                remoteEndPoint = new IPEndPoint(ipAdress, 1669);

                socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEndPoint);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket error. Client could not connect to the server.");
                connectionStatus = "Not connected to server.";
            }
        }
    }
}
