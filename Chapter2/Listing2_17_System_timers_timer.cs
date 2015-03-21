using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_17_System_timers_timer : IListingBase
    {        
        static long całkowitaIlośćPrób = 0L;
        private const int IleWatkow = 100;
        private const long ilośćPróbWWątku = 1000000L;
        private static long całkowitaIlośćTrafień = 0L;
        private static double pi = 0;

        private static EventWaitHandle[] ewht = new EventWaitHandle[IleWatkow];

        public void Start()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            ThreadPool.SetMaxThreads(30, 100);
            WaitCallback metodaWatku = UruchamianieObliczenPi;
            for (int i = 0; i < IleWatkow; ++i)
            {
                ewht[i] = new EventWaitHandle(false, EventResetMode.AutoReset);
                ThreadPool.QueueUserWorkItem(metodaWatku, i);
            }

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(
             (object sender, System.Timers.ElapsedEventArgs e) =>
             {
                 Console.WriteLine("Ilość prób: " +
                 Interlocked.Read(ref całkowitaIlośćPrób).ToString() +
                 "/" + (IleWatkow * ilośćPróbWWątku).ToString());
             });
            timer.Start();

            for (int i = 0; i < IleWatkow; ++i)
                ewht[i].WaitOne();
            double pi = 4.0 * całkowitaIlośćTrafień / (ilośćPróbWWątku * IleWatkow);
            OutputProvider.ShowAllThreadsEndMessage(pi);
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            OutputProvider.ShowTime(roznica);

            timer.Stop();
            timer.Dispose();
        }

        private static void UruchamianieObliczenPi(object parametr)
        {
            try
            {
                int? indeks = parametr as int?;
                OutputProvider.ShowStartLabel(indeks);
                var ilośćTrafień = PiCalculator.ObliczIlośćTrafień(ilośćPróbWWątku, ref całkowitaIlośćPrób);
                Interlocked.Add(ref całkowitaIlośćTrafień, ilośćTrafień);
                ewht[indeks.Value].Set();
            }
            catch (ThreadAbortException ex)
            {
                OutputProvider.ShowThreadAbortException(ex.Message);
            }
            catch (Exception ex)
            {
                OutputProvider.ShowErrorMessage(ex.Message);
            }
        }
    }   
}