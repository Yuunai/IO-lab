using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace IO_lab
{
    class Zad2
    {
        public Zad2()
        {
            ThreadPool.QueueUserWorkItem(serverThread);
            ThreadPool.QueueUserWorkItem(clientThread, new object[] { "Moja wiadomosc!" });
            ThreadPool.QueueUserWorkItem(clientThread, new object[] { "Tez wiadomosc, ale inna!" });
            Thread.Sleep(10000);
        }

        static void clientThread(Object stateInfo)
        {
            byte[] buffer;

            var message = ((object[])stateInfo)[0];
            int messageLength = ((string)message).Length;

            buffer = Encoding.UTF8.GetBytes((string)message);

            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            client.GetStream().Write(buffer, 0, messageLength);


            buffer = new byte[1024];

            int len = client.GetStream().Read(buffer, 0, 1024);
            String s = Encoding.ASCII.GetString(buffer, 0, len);
            Console.WriteLine("Message: " + s);//Encoding.UTF8.GetString(buffer));
        }

        static void serverThread(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                byte[] buffer = new byte[1024];
                int len = client.GetStream().Read(buffer, 0, 1024);
                client.GetStream().Write(buffer, 0, len);
                client.Close();

            }
        }

    }
}
