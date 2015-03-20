using System;

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

        public static void ShowTime(int roznica)
        {
            Console.Write("Czas obliczeń: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(roznica);
            Console.ResetColor();
            Console.WriteLine(" (milisekund)");
        }
    }
}
