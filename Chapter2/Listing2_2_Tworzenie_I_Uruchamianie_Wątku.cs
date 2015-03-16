using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_2_Tworzenie_I_Uruchamianie_Wątku : ListingBase
    {
        public override void Start()
        {
            Thread t = new Thread(UruchamianieObliczenPi);
            t.Start();
            Console.WriteLine("Czy ten napis pojawi się przed otrzymaniem wyniku?");
        }
    }
}