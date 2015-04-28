using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;
using System.Diagnostics;

namespace Fhnw.Ecnf.RoutePlanner.GarbageTest
{
    public class GT
    {
        static Stopwatch time = new Stopwatch();
        
        static void Main(string[] args)
        {
            Console.WriteLine("GC is Server: " + System.Runtime.GCSettings.IsServerGC);
            long average = 0;
            int rounds = 200;
            for (int i = 0; i < rounds; i++)
            {
                time.Start();
                MakeGarbage();
                GC.Collect();
                time.Stop();
                average += time.ElapsedMilliseconds;
                Console.WriteLine("Runde "+i+": " + time.ElapsedMilliseconds);
            }
            Console.WriteLine("Average: " + (average / rounds));
            Console.ReadKey();
        }
        /*
         *Den GC Mode wird im App.config vom GarbageTest geändert false|true
         *Mit Concurrency dauert es 0 - 28 Millisekunden, im Durchschnitt 15 von 200 Durchläufen sind es 15 Millisekunden.
         *Ohne Concurrency dauert es 0 - 41 Millisekunden, im Durchschnitt von 200 Durchläufen sind es 24 Millisekunden.
         */

        public static void MakeGarbage()
        {
            for (var i = 0; i < 500; i++)
            {
                var j = new DateTimeOffset();
                var k = "Bananengas!";
                var l = i;
                var m = true;
            }
        }
    }
}
