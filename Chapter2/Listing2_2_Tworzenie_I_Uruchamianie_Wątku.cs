using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_2_Tworzenie_I_Uruchamianie_Wątku : IListingBase
    {
        private const long IlośćPrób = 10000000L;

        public void Start()
        {
            Thread t = new Thread(UruchamianieObliczenPi);
            t.Start();
            //Thread.Sleep(2000);
            OutputProvider.ShowQuestion();
        }

        private void UruchamianieObliczenPi()
        {
            int czasPoczatkowy = Environment.TickCount;
            OutputProvider.ShowStartLabel();
            double pi = PiCalculator.ObliczPi(IlośćPrób);
            OutputProvider.ShowResultWithThreadNumber(pi);
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            OutputProvider.ShowTime(roznica);
        }
    }
}