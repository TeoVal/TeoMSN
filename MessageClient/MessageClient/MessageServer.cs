using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MessageClient
{
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs args);

    class MessageServerConnector
    {
        private Socket socket = null;
        private IPEndPoint remoteEndPoint = null;
        private ASCIIEncoding asciiEncoding = new ASCIIEncoding();
        private const int rxBuffSize = 1024;
        private byte[] rxBuffer = new byte[rxBuffSize];
        public event MessageReceivedEventHandler MessageReceived;

        public void SendMessage(string message)
        {
            byte[] messageBytes = new byte[1500];
            messageBytes = asciiEncoding.GetBytes(message);
            socket.Send(messageBytes);
        }

        public void StartReceive()
        {
            socket.BeginReceive(rxBuffer, 0, rxBuffer.Length, SocketFlags.None, ReceiveCallBack, null);
        }

        private void ReceiveCallBack(IAsyncResult asyncResult)
        {
            try
            {
                // Suspend RX
                int rxByteCount = socket.EndReceive(asyncResult);

                // Get the data
                string message = Encoding.ASCII.GetString(rxBuffer, 0, rxByteCount);

                // Resume RX
                StartReceive();
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message));
            }
            catch (SocketException ex)
            {
                //ToDo: call Disconnect method
            }
        }

        public bool IsConnected()
        {
            // The short version:
            //return !((socket.Poll(1000, SelectMode.SelectRead) && (socket.Available == 0)) || !socket.Connected);

            bool pollStatus = socket.Poll(1000, SelectMode.SelectRead);
            bool socketStatus = (socket.Available == 0);
            if ((pollStatus && socketStatus) || !socket.Connected)
            {
                return false;
            }
            else
            {
                return true;
            }
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
                StartReceive();
            }
            catch (SocketException ex)
            {
                connectionStatus = "Not connected to server.";
            }
        }
    }
}
