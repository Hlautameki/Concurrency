using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_10_Sekcje_krytyczne_lock : IListingBase
    {
        const int IleWatkow = 10;
        static double _pi = 0; //zmienna współdzielona

        protected const long IlośćPrób = 1000000000L;

        public void Start()
        {
            int czasPoczatkowy = Environment.TickCount;
            Thread[] tt = new Thread[IleWatkow];
            for (int i = 0; i < IleWatkow; ++i)
            {
                tt[i] = new Thread(UruchamianieObliczenPi);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start();
            }

            //czekanie na zakończenie wątków
            foreach (Thread t in tt)
            {
                t.Join();
                OutputProvider.ShowThreadEndMessage(t.ManagedThreadId);
            }
            _pi /= IleWatkow;
            OutputProvider.ShowAllThreadsEndMessage(_pi);
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            OutputProvider.ShowTime(roznica);
        }

        private void UruchamianieObliczenPi()
        {
            try
            {                
                OutputProvider.ShowStartLabelOneLine();
                double pi = PiCalculator.ObliczPiWithInnerRandomGeneratorAndLockSection(IlośćPrób / IleWatkow, _pi);
                _pi += pi;
                OutputProvider.ShowResultWithThreadNumber(pi);
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
