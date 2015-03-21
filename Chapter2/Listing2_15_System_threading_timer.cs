using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_15_System_threading_timer : IListingBase
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

            Timer timer = new Timer((object state) =>
            {
                Console.WriteLine("Ilość prób: " + Interlocked.Read(ref całkowitaIlośćPrób).ToString() + "/" + (IleWatkow * ilośćPróbWWątku).ToString());

            }, //metoda typu TimerCallback (void (object))
                null, //obiekt przesyłany do metody jako argument (state)
                0, //opóźnienie (czas do pierwszego uruchomienia metody)
                1000); //co ile milisekund uruchamiana będzie metoda

            for (int i = 0; i < IleWatkow; ++i)
                ewht[i].WaitOne();
            double pi = 4.0 * całkowitaIlośćTrafień / (ilośćPróbWWątku * IleWatkow);
            OutputProvider.ShowAllThreadsEndMessage(pi);
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            OutputProvider.ShowTime(roznica);

            timer.Change(-1, System.Threading.Timeout.Infinite);
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
