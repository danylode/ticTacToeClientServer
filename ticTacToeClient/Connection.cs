using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ticTacToeClient
{
    public class Connection
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        public void Connect(string ipAddres, int port)
        {
            try
            {
                client = new TcpClient(ipAddres, port);
                stream = client.GetStream();
                reader = new StreamReader(stream);
                writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                Console.WriteLine($"Connected to {ipAddres}:{port}");
                char symbol = Console.ReadLine()[0];
                SendMessage(symbol.ToString());
                Procces();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public virtual string WaitMessage()
        {
            string message;
            do
            {
                message = reader.ReadLine();
            }while(stream.DataAvailable);
            return message;
        }

        public virtual void SendMessage(string message)
        {
            writer.WriteLine(message);
        }

        public virtual void Procces(){
            while(true){
                Console.WriteLine(WaitMessage());
            }
        }
    }
}