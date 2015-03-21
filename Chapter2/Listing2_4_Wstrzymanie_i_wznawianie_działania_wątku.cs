using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_4_Wstrzymanie_i_wznawianie_działania_wątku : IListingBase
    {
        private const long IlośćPrób = 10000000L;

        public void Start()
        {
            Thread t = new Thread(UruchamianieObliczenPi);
            t.Start();
            Thread.Sleep(500);
            t.Suspend();
            OutputProvider.AskForContinuation();
            t.Resume();            
        }

        private void UruchamianieObliczenPi()
        {
            try
            {
                int czasPoczatkowy = Environment.TickCount;
                OutputProvider.ShowStartLabel();
                double pi = PiCalculator.ObliczPi(IlośćPrób);
                OutputProvider.ShowResultWithThreadNumber(pi);
                int czasKoncowy = Environment.TickCount;
                int roznica = czasKoncowy - czasPoczatkowy;
                OutputProvider.ShowTime(roznica);
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
