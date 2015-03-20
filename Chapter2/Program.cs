using System;

namespace Chapter2
{
    class Program
    {
        static int _listeningNumber;

        private const string KodNiezrownoleglony = "Kod niezrównoleglony";
        private const string TworzenieIUruchamianieWatku = "Tworzenie i Uruchamianie wątku";
        private const string PrzerywanieDzialaniaWatku = "Przerywanie działania wątku";
        private const string WstrzymywanieIWznawianieDzialaniaWatku = "Wstrzymywanie i wznawianie działania wątku";
        private const string WatkiDzialajaceWTle = "Wątki działające w tle";
        private const string ZmianaPriorytetuWatku = "Zmiana priorytetu wątku";
        private const string ProblemyZGenerowaniemLiczbPseudolosowych = "Problemy z generowaniem liczb pseudolosowych";
        private const string OsobnyGeneratorLiczbLosowychNaWatek = "Osobny generator liczba losowych na wątek";
        private const string CzekanieNaUkonczeniePracyWatkuMetodaJoin = "Czekanie na ukończenie pracy wątku metoda join";
        private const string SekcjeKrytyczne = "Sekcje krytyczne";
        private const string PrzesylanieDanychDoWatku = "Przesyłanie danych do wątku";
        private const string PulaWatkow = "Pula wątków";
        private const string SygnalizacjaZakonczeniaPracyWatku = "Sygnalizacja zakończenia pracy wątku";
        private const string OperacjeAtomowe = "Operacje atomowe";
        private const string SystemThreadingTimers = "System.Threading.Timers";
        private const string SystemTimersTimer = "System.Timers.Timer";
        private const string Abort = "Abort";
        private const string Mask = "{0}: {1}";
        private static bool _exit = false;

        static void Main(string[] args)
        {
            //ProcessStartInfo pi = new ProcessStartInfo(@"C:\Program Files\ConEmu\ConEmu\ConEmuC64.exe", "/AUTOATTACH");
            //pi.CreateNoWindow = false;
            //pi.UseShellExecute = false;
            //Console.WriteLine("Press Enter after attach succeeded");
            //Start.Start(pi);

            while (!_exit)
            {
                Console.Clear();
                GetListeningNumber();
                Console.Clear();
                if (!_exit)
                    TryStart();
            }
        }

        private static void GetListeningNumber()
        {
            ShowListeningsList();
            Console.Write("Podaj nr listeningu: ");
            var line = Console.ReadLine();
            if (!int.TryParse(line, out _listeningNumber))
                _exit = true;
        }

        private static void ShowListeningsList()
        {
            Console.WriteLine(Mask, 1, KodNiezrownoleglony);
            Console.WriteLine(Mask, 2, TworzenieIUruchamianieWatku);
            Console.WriteLine(Mask, 3, PrzerywanieDzialaniaWatku);
            Console.WriteLine(Mask, 4, WstrzymywanieIWznawianieDzialaniaWatku);
            Console.WriteLine(Mask, 5, WatkiDzialajaceWTle);
            Console.WriteLine(Mask, 6, ZmianaPriorytetuWatku);
            Console.WriteLine(Mask, 7, ProblemyZGenerowaniemLiczbPseudolosowych);
            Console.WriteLine(Mask, 8, OsobnyGeneratorLiczbLosowychNaWatek);
            Console.WriteLine(Mask, 9, CzekanieNaUkonczeniePracyWatkuMetodaJoin);
            Console.WriteLine(Mask, 10, SekcjeKrytyczne);
            Console.WriteLine(Mask, 11, PrzesylanieDanychDoWatku);
            Console.WriteLine(Mask, 12, PulaWatkow);
            Console.WriteLine(Mask, 13, SygnalizacjaZakonczeniaPracyWatku);
            Console.WriteLine(Mask, 14, OperacjeAtomowe);
            Console.WriteLine(Mask, 15, SystemThreadingTimers);
            Console.WriteLine(Mask, 16, SystemTimersTimer);
            Console.WriteLine(Mask, 17, Abort);
            Console.WriteLine("Enter: wyjście");
        }

        private static void TryStart()
        {
            IListingBase listing = null;

            switch (_listeningNumber)
            {
                case 1: listing = new Listing2_1_Kod_niezrównoleglony();
                    break;
                case 2: listing = new Listing2_2_Tworzenie_I_Uruchamianie_Wątku();
                    break;
                case 3: listing = new Listing2_3_Przerywanie_działania_wątku();
                    break;
                case 4: listing = new Listing2_4_Wstrzymanie_i_wznawianie_działania_wątku();
                    break;
                case 5: listing = new Listing2_5_Wątki_działające_w_tle();
                    break;
                case 6: listing = new Listing2_6_Zmiana_Priorytetu_Wątku();
                    break;
                case 7: listing = new Listing2_7_Problemy_z_generowaniem_liczb_pseudolosowych();
                    break;
                case 8: listing = new Listing2_8_Osobny_generator_liczb_losowych_na_wątek();
                    break;
                case 9: listing = new Listing2_9_Czekanie_na_ukończenie_pracy_wątku_metoda_JOIN();
                    break;
                case 10: listing = new Listing2_10_Sekcje_krytyczne_lock();
                    break;
                case 11: listing = new Listing2_11_Przesyłanie_danych_do_wątku();
                    break;
                case 12: listing = new Listing2_12_Pula_wątków();
                    break;
                case 13: listing = new Listing2_13_Sygnalizacja_zakończenia_pracy_wątku();
                    break;                
            }

            if (listing == null) 
                return;
            listing.Start();
            Console.ReadKey();
        }
    }
}