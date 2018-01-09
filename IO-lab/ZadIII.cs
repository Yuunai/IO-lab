using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace IO_lab
{
    class ZadIII
    {
        
        static async Task writeToFile(string fileName, byte[] buffer)
        {
            using (StreamWriter writer = File.CreateText(fileName))
            {
                await writer.WriteAsync(Encoding.ASCII.GetString(buffer));
            }
                
        }
        

        public ZadIII()
        {
            byte[] array = { 71, 65, 66, 70, 72, 75, 79};

            writeToFile("file.txt", array).ContinueWith(
                (t) =>
                {
                     Console.WriteLine("Wiadomość zapisana!");
                });
        }
    }
}
