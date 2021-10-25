using System;
using System.Text.Json;

namespace ticTacToeClient
{
    public class GameConnection : Connection
    {
        public void DrawGrid(char[] grid)
        {
            Console.Clear();
            Console.WriteLine("-+---+---+---+-");
            int currentIndex = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" | " + grid[currentIndex]);
                    if (j == 2)
                    {
                        Console.Write(" | ");
                        Console.Write(Environment.NewLine);
                    }
                    currentIndex++;
                }
                Console.WriteLine("-+---+---+---+-");
            }
        }

        public override void Procces()
        {
            char[] grid = new char[9];
            while (true)
            {
                string state = WaitMessage();
                switch (state)
                {
                    case "Win":
                        Console.WriteLine("You are winner");
                        break;
                    case "Lose":
                        Console.WriteLine("You are lose");
                        break;
                    case "Draw":
                        Console.WriteLine("Draw");
                        break;
                    case "NextTurn":
                        Console.WriteLine("Next turn!");
                        Console.Write("Input (x, y): ");
                        string[] input = Console.ReadLine().Split(' ');
                        var (posX, posY) = (Int32.Parse(input[0]), Int32.Parse(input[1]));
                        SendMessage(JsonSerializer.Serialize<int[]>(new int[] { posX, posY }));
                        break;
                    case "Wait":
                        Console.WriteLine("Wait...");
                        break;
                    case "GridUpdate":
                        grid = JsonSerializer.Deserialize<char[]>(WaitMessage());
                        DrawGrid(grid);
                        break;
                }


            }

        }
    }
}