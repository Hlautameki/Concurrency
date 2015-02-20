using System.Threading;

namespace Chapter2
{
    public class Listing2_6_Zmiana_Priorytetu_Wątku : ListingBase
    {
        public override void Process()
        {
            Thread t = new Thread(UruchamianieObliczenPi);
            t.Priority = ThreadPriority.Highest;
            t.Start();            
        }
    }
}
