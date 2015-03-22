using System;
using System.Threading;

namespace Chapter2
{
    public static class PiCalculator
    {
        public static readonly Random Random = new Random();

        public static double ObliczPi(long ilośćPrób)
        {
            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                var x = Random.NextDouble();
                var y = Random.NextDouble();
                if (x * x + y * y < 1) 
                    ++ilośćTrafień;
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }

        public static double ObliczPiWithInnerRandomGenerator(long ilośćPrób)
        {
            Random r = new Random(Random.Next() & DateTime.Now.Millisecond);

            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                var x = r.NextDouble();
                var y = r.NextDouble();
                if (x * x + y * y < 1)
                    ++ilośćTrafień;
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }

        public static long ObliczIlośćTrafień(long ilośćPrób)
        {
            Random r = new Random(Random.Next() & DateTime.Now.Millisecond);

            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                var x = r.NextDouble();
                var y = r.NextDouble();
                if (x * x + y * y < 1)
                    ++ilośćTrafień;
            }
            return ilośćTrafień;
        }

        public static long ObliczIlośćTrafień(long ilośćPrób, ref long całkowitaIlośćPrób)
        {
            Random r = new Random(Random.Next() & DateTime.Now.Millisecond);

            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                var x = r.NextDouble();
                var y = r.NextDouble();
                if (x * x + y * y < 1)
                    ++ilośćTrafień;
                Interlocked.Increment(ref całkowitaIlośćPrób);
            }
            return ilośćTrafień;
        }

        public static double ObliczPiWithInnerRandomGeneratorAndLockSection(long ilośćPrób, double pi)
        {
            Random r = new Random(Random.Next() & DateTime.Now.Millisecond);

            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                if (i == ilośćPrób / 2)
                {
                    lock ((object)pi) //pudełkowanie
                    {
                        OutputProvider.ShowSyncMessage();
                    }
                }
                var x = r.NextDouble();
                var y = r.NextDouble();
                if (x * x + y * y < 1)
                    ++ilośćTrafień;
            }
            return 4.0 * ilośćTrafień / ilośćPrób;
        }
    }
}
