using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_12
    {
        static Random r = new Random();
        const int ileWatkow = 100;
        static double pi = 0;

        public static void Process()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            WaitCallback metodaWatku = uruchamianieObliczenPi;
            ThreadPool.SetMaxThreads(30, 100);
            for (int i = 0; i < ileWatkow; ++i)
            {
                ThreadPool.QueueUserWorkItem(metodaWatku, i);
            }
            //czekanie na zakończenie wątków
            int ileDostepnychWatkowWPuli = 0; //nieużywane wątki puli
            int ileWszystkichWatkowWPuli = 0; //wszystkie wątki puli
            int ileDzialajacychWatkowPuli = 0; //używane wątki puli
            int tmp = 0;
            do
            {
                ThreadPool.GetAvailableThreads(out ileDostepnychWatkowWPuli, out tmp);
                ThreadPool.GetMaxThreads(out ileWszystkichWatkowWPuli, out tmp);
                ileDzialajacychWatkowPuli = ileWszystkichWatkowWPuli - ileDostepnychWatkowWPuli;
                Console.WriteLine("Ilość aktywnych wątków puli: {0}", ileDzialajacychWatkowPuli);
                Thread.Sleep(1000);
            }
            while (ileDzialajacychWatkowPuli > 0);
            pi /= ileWatkow;
            Console.WriteLine("Wszystkie wątki zakończyły dział anie.\nUśrednione Pi={0}, błąd={1}", pi, Math.Abs(Math.PI - pi));
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            Console.WriteLine("Czas obliczeń: " + (roznica).ToString());
        }

        static void uruchamianieObliczenPi(object parametr)
        {
            try
            {
                int? indeks = parametr as int?;
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0} , indeks {1}...",
                Thread.CurrentThread.ManagedThreadId,
                indeks.HasValue ? indeks.Value.ToString() : "---");
                long ilośćPrób = 10000000L / ileWatkow;
                double pi = obliczPi(ilośćPrób: ilośćPrób);
                Listing2_12.pi += pi;
                Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}", pi,
                Math.Abs(Math.PI - pi),
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
            Random r = new Random(Listing2_12.r.Next() & DateTime.Now.Millisecond);
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
