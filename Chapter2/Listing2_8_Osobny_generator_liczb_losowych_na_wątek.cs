using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_8_Osobny_generator_liczb_losowych_na_wątek : IListingBase
    {
        const int IleWatkow = 10;
        private const long IlośćPrób = 10000000L;

        public void Start()
        {
            Thread[] tt = new Thread[IleWatkow];
            for (int i = 0; i < IleWatkow; ++i)
            {
                tt[i] = new Thread(UruchamianieObliczenPi);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start();
            }
        }

        private void UruchamianieObliczenPi()
        {
            try
            {                
                OutputProvider.ShowStartLabelOneLine();
                double pi = PiCalculator.ObliczPiWithInnerRandomGenerator(IlośćPrób / IleWatkow);
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
