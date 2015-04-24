using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;

namespace Fhnw.Ecnf.RoutePlanner.GarbageTest
{
    public class GT
    {
        static void Main(string[] args)
        {
            MakeGarbage();
            DateTime start = new DateTime();
            GC.Collect();
            DateTime end = new DateTime();
            Console.WriteLine("Normal: " + (end-start));
            start = new DateTime();
            GC.Collect(0);
            end = new DateTime();
            Console.WriteLine("Generation 0: " + (end - start));
            start = new DateTime();
            GC.Collect(1);
            end = new DateTime();
            Console.WriteLine("Generation 1: " + (end - start));
            start = new DateTime();
            GC.Collect(2);
            end = new DateTime();
            Console.WriteLine("Generation 2: " + (end - start));
            start = new DateTime();
            GC.Collect(0, GCCollectionMode.Optimized);
            end = new DateTime();
            Console.WriteLine("Optimized Generation 0: " + (end - start));
            start = new DateTime();
            GC.Collect(1, GCCollectionMode.Optimized);
            end = new DateTime();
            Console.WriteLine("Optimized Generation 1: " + (end - start));
            start = new DateTime();
            GC.Collect(2, GCCollectionMode.Optimized);
            end = new DateTime();
            Console.WriteLine("Optimized Generation 2: " + (end - start));
            Console.ReadKey();
        }

        public static void MakeGarbage()
        {
            for (var i = 0; i < 100000; i++)
            {
                var j = "strawberry";
            }
        }
    }
}
