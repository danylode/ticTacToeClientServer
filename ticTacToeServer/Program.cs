using System;

namespace ticTacToeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleGameServer server = new SimpleGameServer("127.0.0.1", 4546);

            server.StartServer();
        }
    }
}
