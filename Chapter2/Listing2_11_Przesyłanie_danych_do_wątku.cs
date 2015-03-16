using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_11_Przesyłanie_danych_do_wątku : Listing2_10_Sekcje_krytyczne_lock
    {
        protected override Thread[] WeźWątki()
        {
            Thread[] tt = new Thread[IleWatkow];
            for (int i = 0; i < IleWatkow; ++i)
            {
                tt[i] = new Thread(TryCalculate);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start(i);
            }

            return tt;
        }

        protected virtual void TryCalculate(object parameter)
        {
            try
            {
                UruchamianieObliczenPi(parameter);
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

        public virtual void UruchamianieObliczenPi(object parametr)
        {
            {
                int? indeks = parametr as int?;
                int czasPoczatkowy = Environment.TickCount;
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0} , indeks {1}...", Thread.CurrentThread.ManagedThreadId,
                    indeks.HasValue ? indeks.Value.ToString() : "---");
                Uruchom();
                int czasKoncowy = Environment.TickCount;
                int roznica = czasKoncowy - czasPoczatkowy;
                Console.WriteLine("Czas obliczeń: " + (roznica));
            }
        }
    }
}
