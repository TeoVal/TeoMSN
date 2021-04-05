using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MessageServer
{
    class Server
    {
        List<MessageClient> clients = new List<MessageClient>();
        Socket serverSocket = null;
        bool ServerIsConnected = true;

        public void StartServer()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAdress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAdress, 1669);

            serverSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(1);

            Console.WriteLine("Waiting for a connection...");

            Thread listeningThread = new Thread(ListeningMethod);
            listeningThread.Start();

            Console.ReadKey();
        }

        public void ListeningMethod()
        {
            while (ServerIsConnected)
            {
                Socket clientConnection = serverSocket.Accept();
                string clientIPAddress = "The client with the Ip Address : " + IPAddress.Parse(((IPEndPoint)clientConnection.RemoteEndPoint).Address.ToString());
                Console.WriteLine($"{clientIPAddress} connected to server");

                MessageClient messageClient = new MessageClient(clientConnection);
                clients.Add(messageClient);
                messageClient.BeginRx();
            }
        }

        public void StopServer()
        {
            ServerIsConnected = false;
        }
    }
}
