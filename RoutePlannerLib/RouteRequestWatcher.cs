using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestWatcher
    {
        Dictionary<string, int> requestPerCity = new Dictionary<string, int>();
        public void LogRouteRequests(Object sender, RouteRequestEventArgs e)
        {
            if (!requestPerCity.ContainsKey(e.ToCity))
            {
                requestPerCity[e.ToCity] = 1;
            }
            else
            {
                requestPerCity[e.ToCity] = requestPerCity[e.ToCity] + 1;
            }
            if (!requestPerCity.ContainsKey(e.FromCity))
            {
                requestPerCity[e.FromCity] =  0;
            }
            Console.WriteLine("Current Request State");
            Console.WriteLine("---------------------");
            foreach (KeyValuePair<string, int> city in requestPerCity)
            {
                Console.WriteLine("ToCity: {0} has been requested {1} times", city.Key, city.Value);
            }
        }
        public int GetCityRequests(string cityName)
        {
            return requestPerCity[cityName];
        }
    }
}
