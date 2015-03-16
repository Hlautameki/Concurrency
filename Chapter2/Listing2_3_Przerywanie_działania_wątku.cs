using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_3_Przerywanie_działania_wątku : ListingBase
    {
        public override void Start()
        {
            Thread t = new Thread(TryCalculate);
            t.Start();
            Thread.Sleep(500);
            t.Abort();
            Console.WriteLine("Czy ten napis pojawi si ę przed otrzymaniem wyniku?");
        }

        private void TryCalculate()
        {
            try
            {
                UruchamianieObliczenPi();
            }
            catch (ThreadAbortException exc)
            {
                Console.WriteLine("Działanie wątku zostało przerwane (" + exc.Message + ")");
                //Thread.ResetAbort();
                //Calculate();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Wyjątek (" + exc.Message + ")");
            }
        }
    }
}