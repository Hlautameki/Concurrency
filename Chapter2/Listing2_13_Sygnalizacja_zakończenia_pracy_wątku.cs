using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_13_Sygnalizacja_zakończenia_pracy_wątku : IListingBase
    {
        public const int IleWatkow = 100;
        static double _pi = 0; //zmienna współdzielona
        protected const long IlośćPrób = 1000000000L;

        static EventWaitHandle[] ewht = new EventWaitHandle[IleWatkow];

        public void Start()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            WaitCallback metodaWatku = UruchamianieObliczenPi;
            ThreadPool.SetMaxThreads(30, 100);
            for (int i = 0; i < IleWatkow; ++i)
            {
                ewht[i] = new EventWaitHandle(false, EventResetMode.AutoReset);
                ThreadPool.QueueUserWorkItem(metodaWatku, i);
            }
            //czekanie na zakończenie wątków
            for (int i = 0; i < IleWatkow; ++i) 
                ewht[i].WaitOne();
            _pi /= IleWatkow;
            OutputProvider.ShowAllThreadsEndMessage(_pi);
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            OutputProvider.ShowTime(roznica);
        }

        private void UruchamianieObliczenPi(object parametr)
        {
            try
            {
                int? indeks = parametr as int?;
                OutputProvider.ShowStartLabel(indeks);
                double pi = PiCalculator.ObliczPiWithInnerRandomGeneratorAndLockSection(IlośćPrób / IleWatkow, _pi);
                _pi += pi;
                OutputProvider.ShowResultWithThreadNumber(pi);
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
