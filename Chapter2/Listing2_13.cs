using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter2
{
    public class Listing2_13
    {
        static Random r = new Random();
        const int ileWatkow = 100;
        static double pi = 0;

        static EventWaitHandle[] ewht = new EventWaitHandle[ileWatkow];

        public static void Process()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            ThreadPool.SetMaxThreads(30, 100);
            WaitCallback metodaWatku = uruchamianieObliczenPi;
            for (int i = 0; i < ileWatkow; ++i)
            {
                ewht[i] = new EventWaitHandle(false, EventResetMode.AutoReset);
                ThreadPool.QueueUserWorkItem(metodaWatku, i);
            }
            //czekanie na zakończenie wątków
            for (int i = 0; i < ileWatkow; ++i) ewht[i].WaitOne();
            pi /= ileWatkow;
            Console.WriteLine("Wszystkie wątki zakończył y dział anie.\nUśrednione Pi={0}, błąd={1}", pi, Math.Abs(Math.PI - pi));
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            Console.WriteLine("Czas obliczeń: " + (roznica).ToString());
        }

        static void uruchamianieObliczenPi(object parametr)
        {
            try
            {
                int? indeks = parametr as int?;
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}, indeks {1}...",
                Thread.CurrentThread.ManagedThreadId,
                indeks.HasValue ? indeks.Value.ToString() : "---");
                double pi = obliczPi(ilośćPrób: 10000000L);
                Listing2_13.pi += pi;
                Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}",
                pi, Math.Abs(Math.PI - pi),
                Thread.CurrentThread.ManagedThreadId);
                ewht[indeks.Value].Set();
            }
            catch (ThreadAbortException exc)
            {
                Console.WriteLine("Działanie wątku zostało przerwane (" + exc.Message + ")");
            }
            catch (Exception exc)
            {
                Console.WriteLine("Wyjątek (" + exc.Message + ")");
            }
        }

        static double obliczPi(long ilośćPrób)
        {
            Random r = new Random(Listing2_13.r.Next() & DateTime.Now.Millisecond);
            double x, y;
            long ilośćTrafień = 0;
            for (int i = 0; i < ilośćPrób; ++i)
            {
                if (i == ilośćPrób / 2)
                {
                    lock ((object)pi) //pudełkowanie
                    {
                        Console.WriteLine("Synchronizacja: wątek nr {0} osiągnął półmetek",
                        Thread.CurrentThread.ManagedThreadId);
                    }
                }
                x = r.NextDouble();
                y = r.NextDouble();
                if (x * x + y * y < 1) ++ilośćTrafień;
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }
    }
}
