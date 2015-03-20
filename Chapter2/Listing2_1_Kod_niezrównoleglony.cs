using System;

namespace Chapter2
{
    public class Listing2_1_Kod_niezrównoleglony : ListingBase2
    {
        public override void Start()
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