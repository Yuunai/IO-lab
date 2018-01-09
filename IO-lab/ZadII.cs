using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IO_lab
{
    class ZadII
    {
        
        private const int DefaultBufferSize = 4096;

        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;

        public static Task<int[][]> ReadAllLinesAsync(string path)
        {
            return ReadAllLinesAsync(path, Encoding.UTF8);
        }

        public static async Task<int[][]> ReadAllLinesAsync(string path, Encoding encoding)
        {
            var lines = new List<string>();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lines.Add(line);
                }
            }

            var result = lines.Select(x => (x.Split(',').Select(Int32.Parse).ToArray())).ToArray();

            return result;
        }
        
        public ZadII()
        {
            ReadAllLinesAsync(@"../../matrix.txt").ContinueWith(
                (t) => {
                    int[][] table = t.Result;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            Console.Write(table[i][j] + " ");
                        }
                        Console.WriteLine();
                    }
                });
        }   

    }
}
