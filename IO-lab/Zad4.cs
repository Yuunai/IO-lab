using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IO_lab
{
    //zadanie 4 + 5
    class Zad4
    {
        static Object zameczek = new Object();

        static int sum = 0;

        public Zad4()
        {
            Random random = new Random();
            int sumaPewniak = 0;

            int tableSize = 100;
            int pieceSize = 10;


            int[] table = new int[tableSize];
            for (int i = 0; i < tableSize; i++)
            {
                table[i] = random.Next(20);
                sumaPewniak += table[i];
            }

            Console.WriteLine("Suma: " + sumaPewniak);

            for (int i = 0; i < tableSize; i += pieceSize)
            {
                List<int> lista = new List<int>();
                if (i + pieceSize >= tableSize)
                {
                    for (int j = 0; j < tableSize - i; j++)
                    {
                        lista.Add(table[i + j]);
                    }

                }
                else
                {
                    for (int j = 0; j < pieceSize; j++)
                    {
                        lista.Add(table[i + j]);
                    }
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(sumThread), new object[] { lista });
            }


            Console.ReadKey();
        }

        static void sumThread(Object stateInfo)
        {
            Console.WriteLine("Dzialam");
            List<int> lista = (List<int>)(((object[])stateInfo)[0]);

            foreach (int number in lista)
            {
                lock (zameczek)
                {
                    sum += number;
                    Console.WriteLine("Dodana liczba: " + number + " suma: " + sum);
                }

            }

        }

    }

}
