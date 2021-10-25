using System;
using System.Net.Sockets;

namespace ticTacToeClient
{
    class Program
    {

        static void Main(string[] args)
        {
            GameConnection connect = new GameConnection();
            connect.Connect("127.0.0.1", 4546);
        }

    }
}
