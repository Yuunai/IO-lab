using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_lab
{
    class Zad8
    {
        delegate int DelegateType(int arg);

        static DelegateType delegat1;

        static DelegateType delegat2;

        static int silnia(int arg)
        {
            int result = 1;

            for (int i = 1; i <= arg; i++)
            {
                result *= i;
            }

            Console.WriteLine("Iteracyjny: " + result);
            return result;
        }

        static int silniaRek(int arg)
        {
            if (arg == 0 || arg == 1)
            {
                return 1;
            }
            else
            {
                return silniaRek(arg - 1) * arg;
            }
        }

        static int fib(int arg)
        {
            int result = 1;

            if (arg == 0)
                return 0;
            else if (arg == 1)
                return 1;
            else
            {
                int first = 0;
                int second = 1;


                for (int i = 2; i < arg; i++)
                {
                    if (!(i % 2 == 0))
                    {
                        second = result;
                        result += first;
                    }
                    else
                    {
                        first = result;
                        result += second;
                    }
                }
            }

            Console.WriteLine("Iteracyjny: " + result);
            return result;
        }

        static int fibRek(int arg)
        {
            if (arg == 0)
            {
                return 0;
            }
            else if (arg == 1)
            {
                return 1;
            }
            else
            {
                return fibRek(arg - 1) + fibRek(arg - 2);
            }
        }


        public Zad8()
        {
            //Dla silnie podmienić na funkcje silnii
            delegat1 = new DelegateType(fib);

            delegat2 = new DelegateType(fibRek);

            int value = 21;

            IAsyncResult result = delegat1.BeginInvoke(value, null, null);

            IAsyncResult resultRek = delegat2.BeginInvoke(value, null, null);

            int delegateResultRek = delegat2.EndInvoke(resultRek);

            Console.WriteLine("Rekurencyjnie: " + delegateResultRek);

            //Z racji, że EndInvoke jest blokowy, wynik dla funkcji iteracyjnej wyświetlany przed zwróceniem jej wyniku.
            //Może to delikatnie przekłamać wynik, ale przy większych liczbach nie zrobi to większej różnicy.
            int delegateResult = delegat1.EndInvoke(result);

            Console.ReadKey();
        }
    }
}
