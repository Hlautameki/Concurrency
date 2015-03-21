using System;

namespace Chapter2
{
    public class Listing2_1_Kod_niezrównoleglony : IListingBase
    {
        private const long IlośćPrób = 10000000L;

        public void Start()
        {
            UruchamianieObliczenPi();
        }

        private void UruchamianieObliczenPi()
        {
            int czasPoczatkowy = Environment.TickCount;

            double pi = PiCalculator.ObliczPi(IlośćPrób);
            OutputProvider.ShowResult(pi);

            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            OutputProvider.ShowTime(roznica);
        }
    }
}