using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_12_Pula_wątków : Listing2_11_Przesyłanie_danych_do_wątku
    {
        public override int IleWatkow
        {
            get { return 100; }
        }

        public override void Start()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            WaitCallback metodaWatku = TryCalculate;
            ThreadPool.SetMaxThreads(30, 100);
            for (int i = 0; i < IleWatkow; ++i)
            {
                ThreadPool.QueueUserWorkItem(metodaWatku, i);
            }
            //czekanie na zakończenie wątków
            int ileDostepnychWatkowWPuli = 0; //nieużywane wątki puli
            int ileWszystkichWatkowWPuli = 0; //wszystkie wątki puli
            int ileDzialajacychWatkowPuli = 0; //używane wątki puli
            int tmp = 0;
            do
            {
                ThreadPool.GetAvailableThreads(out ileDostepnychWatkowWPuli, out tmp);
                ThreadPool.GetMaxThreads(out ileWszystkichWatkowWPuli, out tmp);
                ileDzialajacychWatkowPuli = ileWszystkichWatkowWPuli - ileDostepnychWatkowWPuli;
                Console.WriteLine("Ilość aktywnych wątków puli: {0}", ileDzialajacychWatkowPuli);
                Thread.Sleep(100);
            }
            while (ileDzialajacychWatkowPuli > 0);

            PokażInfoOZakończeniu(czasPoczatkowy);
        }
    }
}
