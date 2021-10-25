using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.IO;

namespace ticTacToeClient
{
    public class Connection
    {
        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;
        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 4546;

        public Connection(string ipAdress, int port)
        {
            try
            {
                client.Connect(IPAddress.Parse(ipAdress), port);
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
                writer.AutoFlush = true;
                while (true)
                {
                    ConnectionAction state = JsonSerializer.Deserialize<ConnectionAction>(reader.ReadLine());
                    switch (state)
                    {
                        case (ConnectionAction.InputCoords):
                            Console.Write("Input coords: ");
                            string[] input = Console.ReadLine().Split(' ');
                            SendCoords(Int32.Parse(input[0]), Int32.Parse(input[1]));
                            break;
                        case (ConnectionAction.UpdateGrid):
                            Draw(GetGrid());
                            break;
                    }
                    Console.WriteLine(state);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void SendCoords(int x, int y)
        {
            writer.WriteLine($"{x} {y}");
            writer.Flush();
        }

        public char[,] GetGrid()
        {
            string data = reader.ReadLine();
            char[,] grid = JsonSerializer.Deserialize<char[,]>(data);
            return grid;
        }

        private void Draw(char[,] grid)
        {
            Console.Clear();
            Console.WriteLine("-+---+---+---+-");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" | " + grid[i, j]);
                    if (j == 2)
                    {
                        Console.Write(" | ");
                        Console.Write(Environment.NewLine);
                    }
                }
                Console.WriteLine("-+---+---+---+-");
            }

        }

    }
}