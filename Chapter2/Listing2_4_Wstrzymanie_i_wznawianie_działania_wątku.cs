using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_4_Wstrzymanie_i_wznawianie_działania_wątku : ListingBase
    {
        public override void Start()
        {
            Thread t = new Thread(UruchamianieObliczenPi);
            t.Start();
            Thread.Sleep(500);            
            t.Suspend();
            Console.WriteLine("Naciśnij Enter, aby kontynuować działanie wątku...");
            Console.ReadLine();
            t.Resume();
        }
    }
}
