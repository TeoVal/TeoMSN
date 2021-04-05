using System;

namespace MessageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerSocket socket = new ServerSocket();
            socket.StartServer();
        }
    }
}
