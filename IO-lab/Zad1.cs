using System;
using System.Threading;



namespace IO_lab
{
    class Zad1
    {
        public Zad1()
        {

            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 600 });
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 300 });
            Thread.Sleep(2000);
        }

        static void ThreadProc(Object stateInfo)
        {
            var time = ((object[])stateInfo)[0];
            int intTime = (int)time;

            Thread.Sleep(intTime);
            Console.WriteLine("Zostałem wypisanyy po " + intTime + "ms.");
        }

    }
}
