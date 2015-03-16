﻿using System;
using System.Threading;

namespace Chapter2
{
    public abstract class ListingBase
    {
        protected const long IlośćPrób = 10000000L;

        public virtual int IleWatkow
        {
            get { return 10; }
        }

        public abstract void Start();

        public virtual void UruchamianieObliczenPi()
        {
            {
                int czasPoczatkowy = Environment.TickCount;
                Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}...", Thread.CurrentThread.ManagedThreadId);
                Uruchom();
                int czasKoncowy = Environment.TickCount;
                int roznica = czasKoncowy - czasPoczatkowy;
                Console.WriteLine("Czas obliczeń: " + (roznica));
            }
        }

        protected virtual double Uruchom()
        {
            double pi = ObliczPi();
            Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}", pi, Math.Abs(Math.PI - pi), Thread.CurrentThread.ManagedThreadId);
            return pi;
        }

        protected static readonly Random Random = new Random();

        protected virtual double ObliczPi()
        {
            long ilośćTrafień = 0;
            var generator = GetGenerator();
            for (long i = 0; i < WeźIlośćPrób(); ++i)
            {
                if (SprawdźCzyTrafienie(generator))
                    ilośćTrafień++;
            }
            return 4.0 * ilośćTrafień / WeźIlośćPrób();
        }

        protected virtual long WeźIlośćPrób()
        {
            return IlośćPrób;
        }

        protected virtual bool SprawdźCzyTrafienie(Random generator)
        {
            var x = generator.NextDouble();
            var y = generator.NextDouble();
            return x*x + y*y < 1;
            //Console.WriteLine("x={0}, y={1}", x, y);            
        }

        protected virtual Random GetGenerator()
        {
            return Random;
        }
    }
}
