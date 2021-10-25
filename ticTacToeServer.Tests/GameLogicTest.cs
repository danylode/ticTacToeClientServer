using Xunit;
using ticTacToeServer.Game;

namespace ticTacToeServer.Tests
{
    public class GameTests{
        const char PLAYER1 = 'X';
        const char PLAYER2 = 'O';
        const char EMPTYCELL = ' ';

        private GameLogic gameLogic;

        public GameTests(){
            gameLogic = new GameLogic(EMPTYCELL, PLAYER1, PLAYER2);
        }


        [Theory]
        [InlineData(1, 1)]
        [InlineData(3,1)]
        [InlineData(2,3)]
        public void IncrementPlayerIndex(int positionX, int positionY)
        {
            gameLogic.Move(positionX, positionY);
            int playerIndex = gameLogic.ChangeCurrentPlayer();

            Assert.Equal(2, playerIndex);
        }

        [Fact]
        public void Empty_Grid_When_Game_Start()
        {
            var grid = gameLogic.GetGrid();
            char[,] drawGrid = { { EMPTYCELL, EMPTYCELL, EMPTYCELL },
                                { EMPTYCELL, EMPTYCELL, EMPTYCELL },
                                { EMPTYCELL, EMPTYCELL, EMPTYCELL } };

            Assert.Equal(drawGrid, grid);
        }

        [Fact]
        public void Player_Char_When_Change_Player(){
            gameLogic.Move(1,1);
            gameLogic.ChangeCurrentPlayer();
            char playerChar = gameLogic.GetCurrentPlayer();

            Assert.Equal(PLAYER2, playerChar);
        }
    }
}