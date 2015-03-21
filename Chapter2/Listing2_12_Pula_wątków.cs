using System;
using System.Threading;

namespace Chapter2
{
    public class Listing2_12_Pula_wątków : IListingBase
    {
        public const int IleWatkow = 100;
        static double _pi = 0; //zmienna współdzielona
        protected const long IlośćPrób = 1000000000L;

        public void Start()
        {
            int czasPoczatkowy = Environment.TickCount;
            //tworzenie wątków
            WaitCallback metodaWatku = UruchamianieObliczenPi;
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
            _pi /= IleWatkow;

            OutputProvider.ShowAllThreadsEndMessage(_pi);
            int czasKoncowy = Environment.TickCount;
            int roznica = czasKoncowy - czasPoczatkowy;            
            OutputProvider.ShowTime(roznica);
        }

        private void UruchamianieObliczenPi(object parametr)
        {
            try
            {
                int? indeks = parametr as int?;
                OutputProvider.ShowStartLabel(indeks);
                double pi = PiCalculator.ObliczPiWithInnerRandomGeneratorAndLockSection(IlośćPrób / IleWatkow, _pi);
                _pi += pi;
                OutputProvider.ShowResultWithThreadNumber(pi);
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
