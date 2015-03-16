using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_13_Sygnalizacja_zakończenia_pracy_wątku : Listing2_12_Pula_wątków
    {
        static Random r = new Random();
        static double pi = 0;

        static EventWaitHandle[] ewht = new EventWaitHandle[100];

        public override void Start()
        {            
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            ThreadPool.SetMaxThreads(30, 100);
            WaitCallback metodaWatku = TryCalculate;
            for (int i = 0; i < IleWatkow; ++i)
            {
                ewht[i] = new EventWaitHandle(false, EventResetMode.AutoReset);
                
                ThreadPool.QueueUserWorkItem(metodaWatku, i);
                //ewht[i].WaitOne();
            }
            //czekanie na zakończenie wątków
            for (int i = 0; i < IleWatkow; ++i)
                ewht[i].WaitOne();
            PokażInfoOZakończeniu(czasPoczatkowy);
        }

        protected override void TryCalculate(object parametr)
        {
            int? indeks = parametr as int?;
            base.TryCalculate(parametr);
            ewht[indeks.Value].Set();
        }
    }
}
