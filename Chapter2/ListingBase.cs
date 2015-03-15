using System;
using System.Threading;

namespace Chapter2
{
    public abstract class ListingBase
    {
        protected const long ilośćPrób = 10000000L;

        public static int ileWatkow = 10;

        public abstract void Process();

        

        public void UruchamianieObliczenPi()
        {
            {
                int czasPoczatkowy = Environment.TickCount;
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}...", Thread.CurrentThread.ManagedThreadId);
                Uruchom();
                int czasKoncowy = Environment.TickCount;
                int roznica = czasKoncowy - czasPoczatkowy;
                Console.WriteLine("Czas obliczeń: " + (roznica));
            }
        }

        protected virtual double Uruchom()
        {
            double pi = ObliczPi(ilośćPrób);
            Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}", pi, Math.Abs(Math.PI - pi), Thread.CurrentThread.ManagedThreadId);
            return pi;
        }

        protected static readonly Random Random = new Random();

        private double ObliczPi(long ilośćPrób)
        {
            long ilośćTrafień = 0;
            var generator = GetGenerator();
            for (long i = 0; i < ilośćPrób; ++i)
            {
                var x = generator.NextDouble();
                var y = generator.NextDouble();
                if (x * x + y * y < 1) ++ilośćTrafień;
                //Console.WriteLine("x={0}, y={1}", x, y);
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }

        protected virtual Random GetGenerator()
        {
            return Random;
        }
    }
}
