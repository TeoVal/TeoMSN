using System;

namespace MessageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server socket = new Server();
            socket.StartServer();
        }
    }
}
