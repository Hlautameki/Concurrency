using System;

namespace Chapter2
{
    public class Listing2_1
    {
        static Random r = new Random();

        public static void Process()
        {
            uruchamianieObliczenPi();
        }

        static double obliczPi(long ilośćPrób)
        {
            double x, y;
            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                x = r.NextDouble();
                y = r.NextDouble();
                if (x * x + y * y < 1) ++ilośćTrafień;
                //Console.WriteLine("x={0}, y={1}", x, y);
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }

        static void uruchamianieObliczenPi()
        {
            int czasPoczatkowy = Environment.TickCount;
            long ilośćPrób = 10000000L;
            double pi = obliczPi(ilośćPrób: ilośćPrób);
            Console.WriteLine("Pi={0}, błąd={1}", pi, Math.Abs(Math.PI - pi));
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            Console.WriteLine("Czas obliczeń: " + (roznica).ToString());
        }
    }
}
