using System;
using System.Threading;

namespace Chapter2
{
    public class OutputProvider
    {
        public static void ShowResult(double pi)
        {
            Console.Write("{0,0}", "Pi: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", pi);
            Console.ResetColor();            
            Console.Write("{0,8}", "błąd: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Math.Abs(Math.PI - pi));
            Console.ResetColor();
        }

        public static void ShowResultWithThreadNumber(double pi)
        {
            Console.WriteLine("Pi={0}, błąd={1}, wątek nr {2}", pi, Math.Abs(Math.PI - pi), Thread.CurrentThread.ManagedThreadId);
            //Console.Write("{0,0}", "Pi: ");
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.Write("{0}", pi);
            //Console.ResetColor();
            //Console.Write("{0,8}", "błąd: ");
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.Write(Math.Abs(Math.PI - pi));
            //Console.ResetColor();
            //Console.Write("{0,12}", "wątek nr: ");
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            //Console.ResetColor();
        }

        public static void ShowStartLabel()
        {
            Console.Write("Uruchamianie obliczeń, wątek nr ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", Thread.CurrentThread.ManagedThreadId);
            Console.ResetColor();
            Console.WriteLine("...");
        }

        public static void ShowStartLabelOneLine()
        {
            Console.WriteLine("Uruchamianie obliczeń, wątek nr {0}...", Thread.CurrentThread.ManagedThreadId);            
        }

        public static void ShowTime(int roznica)
        {
            Console.Write("Czas obliczeń: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(roznica);
            Console.ResetColor();
            Console.WriteLine(" (milisekund)");
        }

        public static void ShowQuestion()
        {
            Console.WriteLine("Czy ten napis pojawi się przed otrzymaniem wyniku?");
        }

        public static void ShowThreadAbortException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Działanie wątku zostało przerwane (" + message + ")");
            Console.ResetColor();
        }

        public static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wyjątek (" + message + ")");
            Console.ResetColor();
        }

        public static void AskForContinuation()
        {
            Console.WriteLine("Naciśnij Enter, aby kontynuować działanie wątku...");
            Console.ReadLine();
        }

        public static void ShowThreadEndMessage(int threadId)
        {
            Console.WriteLine("Zakończył działanie wątek nr {0}", threadId);
        }

        public static void ShowAllThreadsEndMessage(double pi)
        {
            Console.WriteLine("Wszystkie wątki zakończyły działanie.\nUśrednione Pi={0}, błąd={1}", pi, Math.Abs(Math.PI - pi));
        }

        public static void ShowStartLabel(int? indeks)
        {
            Console.WriteLine("Uruchamianie obliczeń, wątek nr {0} , indeks {1}...", Thread.CurrentThread.ManagedThreadId, indeks.HasValue ? indeks.Value.ToString() : "---");
            //Console.Write("Uruchamianie obliczeń, wątek nr ");
            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.Write("{0}", Thread.CurrentThread.ManagedThreadId);
            //Console.ResetColor();
            //Console.Write("{0,30}", "indeks ");
            //Console.WriteLine("{0}", indeks);
        }

        public static void ShowSyncMessage()
        {
            Console.WriteLine("Synchronizacja: wątek nr {0} osiągnął półmetek", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
