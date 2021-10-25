using System;
using System.Net.Sockets;

namespace ticTacToeClient
{
    class Program
    {

        static void Main(string[] args)
        {
            Connection connect = new Connection("127.0.0.1", 4546);
        }

    }
}
