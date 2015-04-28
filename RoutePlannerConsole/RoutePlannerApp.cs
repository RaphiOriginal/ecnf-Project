using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;
using System.Globalization;
using System.IO;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerConsole
{
    public class RoutePlannerApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(System.Globalization.CultureInfo.CurrentCulture);
            Console.WriteLine("Welcome to RoutePlanner (Version {0})", Assembly.GetExecutingAssembly().GetName().Version);
            var wayPoint = new WayPoint("Windisch", 47.479319847061966, 8.212966918945312);
            Cities bananaRepublik = new Cities();
            //ohne Exception
            bananaRepublik.ReadCities("citiesTestDataLab2.txt");
            foreach (City city in bananaRepublik.FindNeighbours(wayPoint, 5000))
            {
                Console.WriteLine(city.Name);
                Console.WriteLine(city.Location.ToString());
            }
            //mit Exception
            bananaRepublik.ReadCities("exception");
            Console.WriteLine("{0}: {1}/{2}", wayPoint.Name, wayPoint.Latitude, wayPoint.Longitude);
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.ReadKey();
        }
    }
}
