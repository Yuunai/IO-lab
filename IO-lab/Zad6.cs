using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab
{
    class Zad6
    {
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        public Zad6()
        {

            string pathSource = @"../../input.txt";

            FileStream fs = new FileStream(pathSource, FileMode.Open, FileAccess.Read);


            int fileLength = (int)fs.Length;

            byte[] bytes = new byte[fileLength];


            fs.BeginRead(bytes, 0, fileLength, myAsyncCallback, new object[] { fs, bytes });

            autoEvent.WaitOne();

        }

        static void myAsyncCallback(IAsyncResult asyncResult)
        {
            FileStream fileStream = (FileStream)((object[])asyncResult.AsyncState)[0];

            byte[] bytes = (byte[])((object[])asyncResult.AsyncState)[1];

            fileStream.Close();

            Console.WriteLine(Encoding.ASCII.GetString(bytes));

            autoEvent.Set();

        }

    }
}
