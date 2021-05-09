using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;


namespace MessageServer
{
    class MessageClient
    {
        private const int rxBuffSize = 1024;
        private byte[] rxBuffer = new byte[rxBuffSize];
        private Socket socket = null;
        public string ID {get; private set;}

        public MessageClient(Socket socket)
        {
            this.socket = socket;
            ID = Guid.NewGuid().ToString();
        }


        public void BeginRx()
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
                Console.WriteLine(message);

                // Resume RX
                BeginRx();
                BeginTx(message);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket error. Client disconnected from the socket.");
                SocketIsDisconnected();
            }
        }
         
        private void BeginTx(string data)
        {
            byte[] byteTx = Encoding.ASCII.GetBytes(data);
            socket.BeginSend(byteTx, 0, byteTx.Length, 0, new AsyncCallback(SendCallback), socket);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                int bytesSent = socket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client", bytesSent);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool SocketIsDisconnected()
        {
            socket.Close();
            socket.Dispose();
            return true;
        }
    }
}
