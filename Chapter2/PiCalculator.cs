using System;

namespace Chapter2
{
    public static class PiCalculator
    {
        private static readonly Random Random = new Random();

        public static double ObliczPi(long ilośćPrób)
        {
            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                var x = Random.NextDouble();
                var y = Random.NextDouble();
                if (x * x + y * y < 1) ++ilośćTrafień;
                //Console.WriteLine("x={0}, y={1}", x, y);
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }
    }
}
