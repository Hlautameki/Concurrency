using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_7
    {
        static Random r = new Random();
        const int ileWatkow = 10;

        public static void Process()
        {
            Thread[] tt = new Thread[ileWatkow];
            for (int i = 0; i < ileWatkow; ++i)
            {
                tt[i] = new Thread(uruchamianieObliczenPi);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start();
            }
        }

        static void uruchamianieObliczenPi()
        {
            try
            {
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}...",
                Thread.CurrentThread.ManagedThreadId);
                long ilośćPrób = 10000000L / ileWatkow;
                double pi = obliczPi(ilośćPrób: ilośćPrób);
                Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}",
                pi, Math.Abs(Math.PI - pi),
                Thread.CurrentThread.ManagedThreadId);
            }
            catch (ThreadAbortException exc)
            {
                Console.WriteLine("Dział anie wątku zostało przerwane (" + exc.Message + ")");
            }
            catch (Exception exc)
            {
                Console.WriteLine("Wyjątek (" + exc.Message + ")");
            }
        }

        static double obliczPi(long ilośćPrób)
        {
            //Random r = new Random();
            double x, y;
            long ilośćTrafień = 0;
            for (int i = 0; i < ilośćPrób; ++i)
            {
                x = r.NextDouble();
                y = r.NextDouble();
                if (x * x + y * y < 1) ++ilośćTrafień;
                //Console.WriteLine("x={0}, y={1}", x, y);
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }
    }
}
