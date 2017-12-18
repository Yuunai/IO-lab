using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace IO_lab
{
    class Zad3
    {
        static Object zameczek = new object();
        public Zad3()
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

            lock (zameczek)
            {
                writeConsoleMessage("Klient wysyła: " + (string)message, ConsoleColor.Green);
            }
            buffer = new byte[1024];

            client.GetStream().Read(buffer, 0, 1024);
            lock (zameczek)
            {
                writeConsoleMessage("Klient otrzymał: " + Encoding.ASCII.GetString(buffer, 0, messageLength), ConsoleColor.Green);
            }
        }

        static void serverThread(Object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(handleClient, new object[] { client });
                lock (zameczek)
                {
                    writeConsoleMessage("Klient przekazany!", ConsoleColor.Blue);
                }
            }
        }

        static void handleClient(Object stateInfo)
        {
            TcpClient client = (TcpClient)((object[])stateInfo)[0];
            byte[] buffer = new byte[1024];
            int len = client.GetStream().Read(buffer, 0, 1024);

            String s = Encoding.ASCII.GetString(buffer, 0, len);
            lock (zameczek)
            {
                writeConsoleMessage("Serwerowy watek od wiadomosci: " + s, ConsoleColor.Red);
            }

            client.GetStream().Write(buffer, 0, len);
            client.Close();
        }

        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
