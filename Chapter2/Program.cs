﻿using System;
using System.Diagnostics;

namespace Chapter2
{
    class Program
    {
        static int _listeningNumber;

        static void Main(string[] args)
        {
            //ProcessStartInfo pi = new ProcessStartInfo(@"C:\Program Files\ConEmu\ConEmu\ConEmuC64.exe", "/AUTOATTACH");
            //pi.CreateNoWindow = false;
            //pi.UseShellExecute = false;
            //Console.WriteLine("Press Enter after attach succeeded");
            //Process.Start(pi);
            
            GetListeningNumber();
            Console.Clear();

            ListingBase listing;

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
                default: listing = new Listing2_1_Kod_niezrównoleglony();
                    break;
            }

            listing.Process();

            Console.ReadKey();
        }

        private static void GetListeningNumber()
        {
            Console.Write("Podaj nr listeningu: ");
            var line = Console.ReadLine();

            if (!int.TryParse(line, out _listeningNumber))
                GetListeningNumber();
        }
    }
}