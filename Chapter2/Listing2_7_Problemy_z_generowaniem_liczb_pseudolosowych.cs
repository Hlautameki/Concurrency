using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_7_Problemy_z_generowaniem_liczb_pseudolosowych : ListingBase
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
    }
}
