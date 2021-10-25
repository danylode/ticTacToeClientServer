using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using ticTacToeServer.Game;
using System.Linq;

namespace ticTacToeServer
{
    public class GameServer
    {
        private IPAddress iPAddress;
        private int port;

        private List<Player> players = new List<Player>();
        public GameServer(int port)
        {
            this.iPAddress = IPAddress.Loopback;
            this.port = port;
        }
        public GameServer(string ipAddres, int port)
        {
            this.iPAddress = IPAddress.Parse(ipAddres);
            this.port = port;
        }

        public void StartServer()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(iPAddress, port);
                listener.Start();
                while (players.Count != 2)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Player newClient = new Player(client);
                    char symbol = newClient.WaitMessage()[0];
                    newClient.PlayerSymbol = symbol;

                    players.Add(newClient);
                }
                StartGame(players[0], players[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                listener?.Stop();
            }
        }

        public void StartGame(Player player1, Player player2)
        {
            NewGameLogic game = new NewGameLogic(player1.PlayerSymbol, player2.PlayerSymbol);
            State gameState = State.NextTurn;
            Player currentPlayer;
            Player waitPlayer;

            while (true)
            {
                currentPlayer = player1.PlayerSymbol == game.GetCurrentPlayer() ? player1 : player2;
                waitPlayer = currentPlayer == player1? player2: player1;

                GridUpdate(game.GetConvertedGrid(), player1, player2);
                waitPlayer.SendMessage("Wait");
                if (gameState == State.Win)
                {
                    Console.WriteLine("Game Finished");
                    var winner = game.GetCurrentPlayer();
                    if (winner == 0)
                    {
                        players[0].SendMessage("Win");
                        players[1].SendMessage("Lose");
                    }
                    else
                    {
                        players[0].SendMessage("Lose");
                        players[1].SendMessage("Win");
                    }
                    break;
                }
                else if (gameState == State.Draw)
                {
                    player1.SendMessage("Draw");
                    player2.SendMessage("Draw");
                    break;
                }
                else
                {
                    currentPlayer.SendMessage("NextTurn");
                    NextTurn(game, currentPlayer);
                    waitPlayer.SendMessage(" ");
                }
            }

        }

        private void GridUpdate(char[] grid, params Player[] players)
        {
            Console.WriteLine("Send Grid");
            foreach (Player i in players)
            {
                i.SendMessage("GridUpdate");
                i.SendMessage(JsonSerializer.Serialize<char[]>(grid));
            }
        }
        private void NextTurn(NewGameLogic game, Player currentPlayer)
        {
            int[] input = JsonSerializer.Deserialize<int[]>(currentPlayer.WaitMessage());
            try
            {
                Console.WriteLine($"Move {input[0]}:{input[1]}");
                game.NextTurn(input[0], input[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}