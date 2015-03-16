using System.Threading;

namespace Chapter2
{
    public class Listing2_7_Problemy_z_generowaniem_liczb_pseudolosowych : ListingBase
    {
        public override void Start()
        {
            Thread[] tt = WeźWątki();
        }

        protected virtual Thread[] WeźWątki()
        {
            Thread[] tt = new Thread[IleWatkow];
            for (int i = 0; i < IleWatkow; ++i)
            {
                tt[i] = new Thread(UruchamianieObliczenPi);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start();
            }

            return tt;
        }

        protected override long WeźIlośćPrób()
        {
            return IlośćPrób/IleWatkow;
        }
    }
}
