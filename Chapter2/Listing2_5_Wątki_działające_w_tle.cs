using System.Threading;

namespace Chapter2
{
    public class Listing2_5_Wątki_działające_w_tle : ListingBase
    {        
        public override void Start()
        {
            Thread t = new Thread(UruchamianieObliczenPi);
            t.IsBackground = true;
            t.Start();
            Thread.Sleep(500);
        }
    }
}
