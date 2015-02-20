using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_9_Czekanie_na_ukończenie_pracy_wątku_metoda_JOIN : ListingBase
    {
        static Random r = new Random();
        const int ileWatkow = 10;
        static double pi = 0; //zmienna współdzielona
        public override void Process()
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

        private void uruchamianieObliczenPi()
        {
            try
            {
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}...", Thread.CurrentThread.ManagedThreadId);
                long ilośćPrób = 10000000L / ileWatkow;
                double pi = ObliczPi(ilośćPrób: ilośćPrób);
                Listing2_9_Czekanie_na_ukończenie_pracy_wątku_metoda_JOIN.pi += pi;
                Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}", pi, Math.Abs(Math.PI - pi), Thread.CurrentThread.ManagedThreadId);
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
    }
}
