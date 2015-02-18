using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutePlanner (Version {0})", Assembly.GetExecutingAssembly().GetName().Version);
            Console.ReadKey();
        }
    }
}
