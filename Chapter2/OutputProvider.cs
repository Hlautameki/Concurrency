using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2
{
    public class OutputProvider
    {
        public static void ShowResult(double pi)
        {
            Console.WriteLine("Pi={0}, błąd={1}", pi, Math.Abs(Math.PI - pi));
        }

        public static void ShowTime(int roznica)
        {
            Console.WriteLine("Czas obliczeń: " + roznica);
        }
    }
}
