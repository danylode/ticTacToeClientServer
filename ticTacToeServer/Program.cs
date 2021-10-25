using System;

namespace ticTacToeServer{
    public class Program{
        public static void Main(){
            GameServer newServer = new GameServer(4546);
            newServer.StartServer();
        }
    }
}