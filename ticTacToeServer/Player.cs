using System;
using System.Net.Sockets;

namespace ticTacToeServer{
    public class Player: Client
    {
        private char playerSymbol;
        public char PlayerSymbol { get => playerSymbol; set => playerSymbol = value; }
        public Player(TcpClient client): base(client){
        }

 
    }
}