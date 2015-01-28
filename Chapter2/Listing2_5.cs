using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_5
    {
        static Random r = new Random();

        public static void Process()
        {
            Thread t = new Thread(uruchamianieObliczenPi);
            t.IsBackground = true;
            t.Start();
            Thread.Sleep(500);
        }

        private static void uruchamianieObliczenPi()
        {
            int czasPoczatkowy = Environment.TickCount;
            Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}...",
                Thread.CurrentThread.ManagedThreadId);
            long ilośćPrób = 10000000L;
            double pi = obliczPi(ilośćPrób: ilośćPrób);
            Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}",
                pi, Math.Abs(Math.PI - pi),
                Thread.CurrentThread.ManagedThreadId);
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            Console.WriteLine("Czas obliczeń: " + (roznica).ToString());
        }


        static double obliczPi(long ilośćPrób)
        {
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
