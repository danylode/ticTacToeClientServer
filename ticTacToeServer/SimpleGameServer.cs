using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ticTacToeServer.Game;

namespace ticTacToeServer
{
    public class SimpleGameServer
    {
        private IPAddress iPAddress;
        private int port;

        private List<PlayerObject> players = new List<PlayerObject>();
        public SimpleGameServer(string ip, int port)
        {
            this.iPAddress = IPAddress.Parse(ip);
            this.port = port;
        }

        public void StartServer()
        {
            TcpListener server = null;
            try
            {
                server = new TcpListener(iPAddress, port);
                server.Start();
                Console.WriteLine("Wait players...");
                while (players.Count != 2)
                {
                    TcpClient newPlayer = server.AcceptTcpClient();
                    PlayerObject newPlayerObject = new PlayerObject(newPlayer, 'X');

                    players.Add(newPlayerObject);
                }
                StartGame(' ', players[0], players[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                foreach(PlayerObject player in players)
                    player.CloseConnection();
                server.Stop();
            }
        }

        public void StartGame(char emptyChar, PlayerObject player1, PlayerObject player2){
            GameLogic game = new GameLogic(emptyChar, player1.playerSymbol, player2.playerSymbol);
            State gameState = State.NextMove;
            int currentPlayer = 1;

            while(gameState != State.Win){
                var (x,y) = players[currentPlayer].WaitCoords();
                player1.SendGrid(game.GetGrid());
                try{
                    game.Move(x,y);
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                    continue;
                }
                gameState = game.GetState();
                game.ChangeCurrentPlayer();
                currentPlayer = game.GetCurrentPlayer();
            }
        }
    }
}