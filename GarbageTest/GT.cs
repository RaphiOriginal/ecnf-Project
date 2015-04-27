using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;

namespace Fhnw.Ecnf.RoutePlanner.GarbageTest
{
    public class GT
    {
        public static DateTime start;
        public static DateTime end;
        public static bool waiting = true;
        public static bool atTheEnd = false;
        
        static void Main(string[] args)
        {
            GC.RegisterForFullGCNotification(99, 99);
            Console.WriteLine("GC is Server: " + System.Runtime.GCSettings.IsServerGC);
            var garbageChecker = new Thread(Monitoring);
            garbageChecker.Start();
            /*MakeGarbage();
            start = new DateTime();
            GC.Collect();
            Waiting();
            Conole.WriteLine("Normal: " + (end.Millisecond-start.Millisecond));*/
            MakeGarbage();
            start = new DateTime();
            GC.Collect(0, GCCollectionMode.Default, true);
            Waiting();
            Console.WriteLine("Generation 0: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(1, GCCollectionMode.Default, true);
            Waiting();
            Console.WriteLine("Generation 1: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(2, GCCollectionMode.Default, true);
            Waiting();
            Console.WriteLine("Generation 2: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(0, GCCollectionMode.Optimized, true);
            Waiting();
            Console.WriteLine("Optimized Generation 0: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(1, GCCollectionMode.Optimized, true);
            Waiting();
            Console.WriteLine("Optimized Generation 1: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(2, GCCollectionMode.Optimized, true);
            Waiting();
            Console.WriteLine("Optimized Generation 2: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(0, GCCollectionMode.Forced, true);
            Waiting();
            Console.WriteLine("Forced Generation 0: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(1, GCCollectionMode.Forced, true);
            Waiting();
            Console.WriteLine("Forced Generation 1: " + (end.Millisecond - start.Millisecond));
            MakeGarbage();
            start = new DateTime();
            GC.Collect(2, GCCollectionMode.Forced, true);
            Waiting();
            atTheEnd = true;
            Console.WriteLine("Forced Generation 2: " + (end.Millisecond - start.Millisecond));
            Console.ReadKey();
        }

        public static void MakeGarbage()
        {
            for (var i = 0; i < 500; i++)
            {
                var j = new DateTimeOffset();
                var k = "Bananengas!";
                var l = i;
            }
        }
        public static void Monitoring()
        {
            
            while (!atTheEnd)
            {
                GCNotificationStatus garbageMonitor = GC.WaitForFullGCApproach();
                if (garbageMonitor == GCNotificationStatus.Succeeded)
                {
                    
                }
                else if (garbageMonitor == GCNotificationStatus.NotApplicable)
                {
                    //Console.WriteLine("GC was not applicable!");
                }
                garbageMonitor = GC.WaitForFullGCComplete();
                if (garbageMonitor == GCNotificationStatus.Succeeded)
                {
                    end = new DateTime();
                    waiting = false;
                }
                else if (garbageMonitor == GCNotificationStatus.NotApplicable)
                {

                }
            }
        }
        public static void Waiting()
        {
            while (waiting) { } //Do nothing
            waiting = true;
        }
    }
}
