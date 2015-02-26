using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    public class RoutePlannerApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to RoutePlanner (Version {0})", Assembly.GetExecutingAssembly().GetName().Version);
            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            Cities bananaRepublik = new Cities();
            bananaRepublik.ReadCities("citiesTestDataLab2.txt");
            foreach (City city in bananaRepublik.FindNeigbours(wayPoint, 5000))
            {
                Console.WriteLine(city.Name);
                Console.WriteLine(city.Location.ToString());
            }
            Console.WriteLine("{0}: {1}/{2}", wayPoint.Name, wayPoint.Latitude, wayPoint.Longitude);
            Console.ReadKey();
        }
    }
}
