using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_8_Osobny_generator_liczb_losowych_na_wątek : ListingBase
    {
        public override void Process()
        {
            Thread[] tt = new Thread[ileWatkow];
            for (int i = 0; i < ileWatkow; ++i)
            {
                tt[i] = new Thread(UruchamianieObliczenPi);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start();
            }
        }

        protected override Random GetGenerator()
        {
            return new Random();
        }
    }
}
