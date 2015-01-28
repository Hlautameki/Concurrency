using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_10
    {
        static Random r = new Random();
        const int ileWatkow = 10;
        static double pi = 0; //zmienna współdzielona

        public static void Process()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            Thread[] tt = new Thread[ileWatkow];
            for (int i = 0; i < ileWatkow; ++i)
            {
                tt[i] = new Thread(uruchamianieObliczenPi);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start();
            }
            //czekanie na zakończenie wątków
            foreach (Thread t in tt)
            {
                t.Join();
                Console.WriteLine("Zakończył działanie wątek nr {0}", t.ManagedThreadId);
            }
            pi /= ileWatkow;
            Console.WriteLine("Wszystkie wątki zakończyły działanie.\nUśrednione Pi={0}, błąd={1}", pi, Math.Abs(Math.PI - pi));
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            Console.WriteLine("Czas obliczeń: " + (roznica).ToString());
        }

        static void uruchamianieObliczenPi()
        {
            try
            {
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}...",
                Thread.CurrentThread.ManagedThreadId);
                long ilośćPrób = 100000000L / ileWatkow;
                double pi = obliczPi(ilośćPrób: ilośćPrób);
                lock (r) Listing2_10.pi += pi; // r nie jest blokowany
                Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}",
               pi, Math.Abs(Math.PI - pi),
               Thread.CurrentThread.ManagedThreadId);
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
            Random r = new Random(Listing2_10.r.Next() & DateTime.Now.Millisecond);
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
