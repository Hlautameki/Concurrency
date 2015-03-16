using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_10_Sekcje_krytyczne_lock : Listing2_9_Czekanie_na_ukończenie_pracy_wątku_metoda_JOIN
    {
        protected override void ZwiększPi(double pi)
        {
            lock (Random) _pi += pi;
        }

        protected override double ObliczPi()
        {
            long ilośćTrafień = 0;
            var generator = GetGenerator();
            for (long i = 0; i < WeźIlośćPrób(); ++i)
            {
                if (i == IlośćPrób / 2)
                {
                    lock ((object)_pi) //pudełkowanie
                    {
                        Console.WriteLine("Synchronizacja: wątek nr {0} osiągnął półmetek",
                        Thread.CurrentThread.ManagedThreadId);
                    }
                }
                if (SprawdźCzyTrafienie(generator))
                    ++ilośćTrafień;
            }
            return 4.0 * ilośćTrafień / WeźIlośćPrób();
        }
    }
}
