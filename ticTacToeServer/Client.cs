using System.IO;
using System.Net.Sockets;

namespace ticTacToeServer{
    public class Client{
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;
        public Client(TcpClient client){
            this.client = client;
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

        }

        public void SendMessage(string message){
            writer.WriteLine(message);
        }

        public string WaitMessage(){
            string message = reader.ReadLine();
            return message;
        }

        public void Disconect(){
            client.Close();
        }
    }
}