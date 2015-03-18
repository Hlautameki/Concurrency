using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_17_System_timers_timer
    {
        private static Random r = new Random();
        static long całkowitaIlośćPrób = 0L;
        private const int ileWatkow = 100;
        private const long ilośćPróbWWątku = 10000000L;
        private static long całkowitaIlośćTrafień = 0L;
        private static double pi = 0;

        private static EventWaitHandle[] ewht = new EventWaitHandle[ileWatkow];

        public static void Process()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            ThreadPool.SetMaxThreads(30, 100);
            WaitCallback metodaWatku = uruchamianieObliczenPi;
            for (int i = 0; i < ileWatkow; ++i)
            {
                ewht[i] = new EventWaitHandle(false, EventResetMode.AutoReset);
                ThreadPool.QueueUserWorkItem(metodaWatku, i);
            }
            for (int i = 0; i < ileWatkow; ++i) ewht[i].WaitOne();
            double pi = 4.0 * całkowitaIlośćTrafień / (ilośćPróbWWątku * ileWatkow);
            Console.WriteLine("Wszystkie wątki zakończyły dział anie.\nUśrednione Pi={0}, błąd={1}", pi,
                Math.Abs(Math.PI - pi));
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            Console.WriteLine("Czas obliczeń: " + (roznica).ToString());

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
             (object sender, System.Timers.ElapsedEventArgs e) =>
             {
                 Console.WriteLine("Ilość prób: " +
                 Interlocked.Read(ref całkowitaIlośćPrób).ToString() +
                 "/" + (ileWatkow * ilośćPróbWWątku).ToString());
             });
            timer.Start();
        }

        private static void uruchamianieObliczenPi(object parametr)
        {
            try
            {
                int? indeks = parametr as int?;
                Console.WriteLine("Uruchamianie obliczeń, watek nr {0}, indeks {1}...",
                    Thread.CurrentThread.ManagedThreadId, indeks.HasValue ? indeks.Value.ToString() : "---");
                long ilośćPrób = 1000000000L / ileWatkow;
                long ilośćTrafień = obliczPi(ilośćPrób: ilośćPróbWWątku);
                pi += pi;
                Interlocked.Add(ref całkowitaIlośćTrafień, ilośćTrafień);
                Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}", pi, Math.Abs(Math.PI - pi),
                    Thread.CurrentThread.ManagedThreadId);
                ewht[indeks.Value].Set();
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

        static long obliczPi(long ilośćPrób)
        {
            Random r = new Random(Listing2_17_System_timers_timer.r.Next() & DateTime.Now.Millisecond);
            double x, y;
            long ilośćTrafień = 0;
            for (long i = 0; i < ilośćPrób; ++i)
            {
                x = r.NextDouble();
                y = r.NextDouble();
                if (x * x + y * y < 1) ++ilośćTrafień;
                Interlocked.Increment(ref całkowitaIlośćPrób);
                //Console.WriteLine("x={0}, y={1}", x, y);
            }
            return ilośćTrafień;
        }
    }
}
