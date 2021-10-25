using System;
using System.Net.Sockets;
using System.Text.Json;
using System.IO;

namespace ticTacToeServer
{
    public class PlayerObject
    {
        private TcpClient playerConnection;
        private StreamWriter writer;
        private StreamReader reader;
        public readonly char playerSymbol;

        public PlayerObject(TcpClient playerConnection, char playerSymbol)
        {
            this.playerConnection = playerConnection;
            this.playerSymbol = playerSymbol;

            writer = new StreamWriter(playerConnection.GetStream());
            writer.AutoFlush = true;
            reader = new StreamReader(playerConnection.GetStream());

            writer.WriteLine("Connected");
        }

        public void CloseConnection(){
            playerConnection.Close();
        }

        public (int, int) WaitCoords(){
            writer.Write(ConnectionAction.InputCoords);
            string[] coords =  reader.ReadLine().Split(' ');
            return (Int32.Parse(coords[0]),Int32.Parse(coords[1]));
        }

        public void SendGrid(char[,] grid){
            writer.Write(ConnectionAction.UpdateGrid);
            string gridToJson = JsonSerializer.Serialize<char[,]>(grid);
            writer.WriteLine(gridToJson);
        }
    }
}