﻿using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_6_Zmiana_Priorytetu_Wątku : IListingBase
    {
        private const long IlośćPrób = 10000000L;
        public  void Start()
        {
            Thread t = new Thread(UruchamianieObliczenPi);
            t.Priority = ThreadPriority.Highest;
            t.Start();
        }

        private void UruchamianieObliczenPi()
        {
            try
            {
                int czasPoczatkowy = Environment.TickCount;
                OutputProvider.ShowStartLabel();
                double pi = PiCalculator.ObliczPi(IlośćPrób);
                OutputProvider.ShowResultWithThreadNumber(pi);
                int czasKoncowy = Environment.TickCount;
                int roznica = czasKoncowy - czasPoczatkowy;
                OutputProvider.ShowTime(roznica);
            }
            catch (ThreadAbortException ex)
            {
                OutputProvider.ShowThreadAbortException(ex.Message);
            }
            catch (Exception ex)
            {
                OutputProvider.ShowErrorMessage(ex.Message);
            }
        }  
    }
}
