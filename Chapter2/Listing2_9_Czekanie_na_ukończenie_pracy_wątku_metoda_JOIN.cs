﻿using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_9_Czekanie_na_ukończenie_pracy_wątku_metoda_JOIN : ListingBase
    {                
        protected static double _pi = 0; //zmienna współdzielona

        public override void Process()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            Thread[] tt = new Thread[ileWatkow];
            for (int i = 0; i < ileWatkow; ++i)
            {
                tt[i] = new Thread(TryCalculate);
                tt[i].Priority = ThreadPriority.Lowest;
                tt[i].Start();
            }
            //czekanie na zakończenie wątków
            foreach (Thread t in tt)
            {
                //Console.WriteLine("Set Join {0}", t.ManagedThreadId);
                t.Join();
                Console.WriteLine("Zakończył działanie wątek nr {0}", t.ManagedThreadId);
            }
            _pi /= ileWatkow;
            Console.WriteLine("Wszystkie wątki zakończyły działanie.\nUśrednione Pi={0}, błąd={1}", _pi, Math.Abs(Math.PI - _pi));
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;
            Console.WriteLine("Czas obliczeń: " + (roznica).ToString());
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
            }
            catch (Exception exc)
            {
                Console.WriteLine("Wyjątek (" + exc.Message + ")");
            }
        }

        protected override double Uruchom()
        {
            var pi = base.Uruchom();
            ZwiększPi(pi);
            return pi;
        }

        protected virtual void ZwiększPi(double pi)
        {
            _pi += pi;
        }

        protected override Random GetGenerator()
        {
            return new Random(Random.Next() & DateTime.Now.Millisecond);
        }
    }
}
