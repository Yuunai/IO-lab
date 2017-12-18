using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_lab
{
    class Zad7
    {
        public Zad7()
        {


            string pathSource = @"../../input.txt";

            FileStream fs = new FileStream(pathSource, FileMode.Open, FileAccess.Read);


            int fileLength = (int)fs.Length;

            byte[] bytes = new byte[fileLength];

            IAsyncResult result = fs.BeginRead(bytes, 0, fileLength, null, new object[] { fs, bytes });

            fs.EndRead(result);

            fs.Close();

            Console.WriteLine(Encoding.ASCII.GetString(bytes));

        }
    }

}
