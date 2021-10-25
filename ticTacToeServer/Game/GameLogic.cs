using System;
using System.Collections.Generic;

namespace ticTacToeServer.Game
{
    public class GameLogic
    {
        private char emptyCell;
        private Dictionary<int, char> players = new Dictionary<int, char>();
        private int currentPlayerIndex = 0;
        private char[,] grid = new char[3, 3];

        public GameLogic(char emptyCell, params char[] players)
        {
            Console.WriteLine(players);
            this.emptyCell = emptyCell;
            foreach (char i in players)         //Add players
            {
                this.players.Add(++currentPlayerIndex, i);
            }
            for (int i = 0; i < 3; i++)         //Initial grid
            {
                for (int j = 0; j < 3; j++)
                {
                    this.grid[i, j] = emptyCell;
                }
            }
            currentPlayerIndex = 1;
        }

        public int ChangeCurrentPlayer()
        {
            if (currentPlayerIndex != players.Count)
            {
                currentPlayerIndex++;
            }
            else
            {
                currentPlayerIndex = 1;
            }
            return currentPlayerIndex;
        }

        public void Move(int x, int y)
        {
            if (grid[x - 1, y - 1] == emptyCell)
            {
                grid[x - 1, y - 1] = players[currentPlayerIndex];
            }
            else
            {
                throw new Exception("Клетка занята");
            }
        }

        public State GetState()
        {
            if (grid[0, 0] == players[currentPlayerIndex] && grid[1, 1] == players[currentPlayerIndex] && grid[2, 2] == players[currentPlayerIndex])
            {
                return State.Win;
            }
            else if (grid[0, 2] == players[currentPlayerIndex] && grid[1, 1] == players[currentPlayerIndex] && grid[2, 0] == players[currentPlayerIndex])
            {
                return State.Win;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (grid[i, 0] == players[currentPlayerIndex] && grid[i, 1] == players[currentPlayerIndex] && grid[i, 2] == players[currentPlayerIndex])
                    {
                        return State.Win;
                    }
                    if (grid[0, i] == players[currentPlayerIndex] && grid[1, i] == players[currentPlayerIndex] && grid[2, i] == players[currentPlayerIndex])
                    {
                        return State.Win;
                    }
                }
            }
            foreach (char i in grid)
            {
                if (i == emptyCell)
                {
                    return State.NextMove;
                }
            }
            return State.Draw;
        }

        public char[,] GetGrid()
        {
            return grid;
        }

        public char GetCurrentPlayer()
        {
            return players[currentPlayerIndex];
        }
    }
}