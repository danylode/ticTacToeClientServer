using System;
using System.Collections.Generic;
using System.Linq;

namespace ticTacToeServer.Game
{
    public class NewGameLogic
    {
        private const char EMPTYCHAR = ' ';
        private char[,] gameGrid = new char[,] {{EMPTYCHAR,EMPTYCHAR,EMPTYCHAR},
                                                {EMPTYCHAR,EMPTYCHAR,EMPTYCHAR},
                                                {EMPTYCHAR,EMPTYCHAR,EMPTYCHAR}};

        private Dictionary<int, char> players = new Dictionary<int, char>();
        private int currentPlayer;
        public NewGameLogic(char player1, char player2)
        {
            players.Add(0, player1);
            players.Add(1, player2);

            currentPlayer = 0;
        }

        public State GetState()
        {
            if (gameGrid[0, 0] == players[currentPlayer] && gameGrid[1, 1] == players[currentPlayer] && gameGrid[2, 2] == players[currentPlayer])
            {
                return State.Win;
            }
            else if (gameGrid[0, 2] == players[currentPlayer] && gameGrid[1, 1] == players[currentPlayer] && gameGrid[2, 0] == players[currentPlayer])
            {
                return State.Win;
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (gameGrid[i, 0] == players[currentPlayer] && gameGrid[i, 1] == players[currentPlayer] && gameGrid[i, 2] == players[currentPlayer])
                    {
                        return State.Win;
                    }
                    if (gameGrid[0, i] == players[currentPlayer] && gameGrid[1, i] == players[currentPlayer] && gameGrid[2, i] == players[currentPlayer])
                    {
                        return State.Win;
                    }
                }
            }
            foreach (char i in gameGrid)
            {
                if (i == EMPTYCHAR)
                {
                    return State.NextTurn;
                }
            }
            return State.Draw;
        }


        public void NextTurn(int posX, int posY)
        {
            posX--;
            posY--;
            if (gameGrid[posX, posX] == EMPTYCHAR)
            {
                gameGrid[posX, posY] = players[currentPlayer];
                ChangePlayer();
            }
            else
            {
                throw new Exception("Cell isn't empty");
            }
        }

        private void ChangePlayer()
        {
            currentPlayer = currentPlayer == 0 ? 1 : 0;
        }

        public char GetCurrentPlayer()
        {
            return players[currentPlayer];
        }

        public char[] GetConvertedGrid()
        {
            char[] grid = new char[gameGrid.Length];
            int index = 0;
            foreach (var i in gameGrid)
            {
                grid[index] = i;
                index++;
            }
            return grid;
        }

    }
}