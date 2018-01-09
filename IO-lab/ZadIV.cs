using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IO_lab
{

    class ZadIV
    {
        class Client
        {
            private TcpClient client;
            private byte[] buffer;
            private static string HEY = "HEY";
            private static string BYE = "BYE";

            public Client()
            {
                //Server messsages length = 3 [HEY/BYE/ACK]
                buffer = new byte[3];
                client = new TcpClient("127.0.0.1", 2048);
                client.GetStream().ReadTimeout = 3000;
                client.GetStream().Write(Encoding.ASCII.GetBytes(HEY), 0, HEY.Length);

                client.GetStream().Read(buffer, 0, buffer.Length);
                if (Encoding.ASCII.GetString(buffer).Trim() != HEY)
                    Console.WriteLine(Encoding.ASCII.GetString(buffer));
            }


            public void sendMessage(string message)
            {
                client.GetStream().Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
                client.GetStream().Read(buffer, 0, 3);

                Console.WriteLine(Encoding.ASCII.GetString(buffer));
            }


            public void closeConnection()
            {
                client.GetStream().Write(Encoding.ASCII.GetBytes(BYE), 0, BYE.Length);
                client.GetStream().Read(buffer, 0, 3);


                Console.WriteLine(Encoding.ASCII.GetString(buffer));
            }
        }

        class Server
        {
            private TcpListener server;

            public Server()
            {
                this.server = new TcpListener(IPAddress.Any, 2048);
                server.Start();

                ThreadPool.QueueUserWorkItem(serverThread, new object[] { server });
            }
            static void serverThread(Object stateInfo)
            {
                var server = (TcpListener)((object[])stateInfo)[0];

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(handleClient, new object[] { client });
                }
            }

            static void handleClient(Object stateInfo)
            {
                TcpClient client = (TcpClient)((object[])stateInfo)[0];
                byte[] buffer = new byte[1024];

                int len = client.GetStream().Read(buffer, 0, 1024);
                String message = Encoding.ASCII.GetString(buffer, 0, len);

                if (message != "HEY")
                {
                    Console.WriteLine("Incorrect connection message! Closing connection...");
                    client.Close();
                }
                else
                {
                    client.GetStream().Write(Encoding.ASCII.GetBytes("HEY"), 0, 3);
                }

                while (message != "BYE")
                {
                    len = client.GetStream().Read(buffer, 0, 1024);
                    message = Encoding.ASCII.GetString(buffer, 0, len);

                    if (message != "BYE")
                    {
                        client.GetStream().Write(Encoding.ASCII.GetBytes("ACK"), 0, 3);
                        Console.WriteLine("Server: " + message);
                    }
                }

                client.GetStream().Write(Encoding.ASCII.GetBytes("BYE"), 0, 3);
                client.Close();
                Console.WriteLine("Client disconnected!");
            }

        }

        public ZadIV()
        {
            Server server = new Server();

            Client client = new Client();

            client.sendMessage("Message from client");

            client.sendMessage("Another message from client");

            client.closeConnection();
        }

    }
    
}
