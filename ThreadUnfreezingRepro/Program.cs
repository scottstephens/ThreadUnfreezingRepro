using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadUnfreezingRepro
{
    class Program
    {
        static volatile int Counter = 0;
        static void Main(string[] args)
        {
#if DEBUG
            if (Environment.GetEnvironmentVariable("DOTNETDEBUG") == "1")
            {
                Console.WriteLine($"Waiting for debugger to attach.");
                while (!Debugger.IsAttached)
                    Thread.Sleep(100);
                Console.WriteLine("Debugger is attached.");
            }
#endif

            ThreadPool.QueueUserWorkItem(Program.Work, 1);
            ThreadPool.QueueUserWorkItem(Program.Work, 2);
            ThreadPool.QueueUserWorkItem(Program.Work, 3);
            ThreadPool.QueueUserWorkItem(Program.Work, 4);

            while (Counter < 4)
                Thread.Sleep(100);
        }

        static void Work(object state)
        {
            for (int ii = 0; ii < 10; ++ii)
                Thread.Sleep(1000);
            Interlocked.Increment(ref Counter);
        }
    }
}
