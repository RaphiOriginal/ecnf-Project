using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesDijkstra:Routes
    {
        public RoutesDijkstra(Cities cities) : base(cities)
        {
        }


        public async Task<List<Link>> FindShortestRouteBetweenAsync(string fromCity, string toCity, TransportModes mode, IProgress<string> progress)
        {
            NotifyObservers(fromCity, toCity, mode);
            if (progress != null) progress.Report("notify Observer done");
            var citiesBetween = FindCitiesBetween(fromCity, toCity);
            if (progress != null) progress.Report("FindcitiesBetween done");
            if (citiesBetween == null || citiesBetween.Count < 1 || routes == null || routes.Count < 1)
                return null;

            var source = citiesBetween[0];
            var target = citiesBetween[citiesBetween.Count - 1];
            if (progress != null) progress.Report("set source and target done");
            Dictionary<City, double> dist;
            Dictionary<City, City> previous;
            var q = FillListOfNodes(citiesBetween, out dist, out previous);
            if (progress != null) progress.Report("FillListOfNodes done");
            dist[source] = 0.0;
            if (progress != null) progress.Report("set Source distance done");

            // the actual algorithm
            previous = SearchShortestPath(mode, q, dist, previous);
            if (progress != null) progress.Report("SearchShortestPath done");

            // create a list with all cities on the route
            var citiesOnRoute = GetCitiesOnRoute(source, target, previous);
            if (progress != null) progress.Report("GetCitiesOnroute done");

            // prepare final list of links
            return FindPath(citiesOnRoute, mode);
            
        }
        public Task<List<Link>> FindShortestRouteBetweenAsync(string fromCity, string toCity, TransportModes mode)
        {
            return FindShortestRouteBetweenAsync(fromCity, toCity, mode, null);
        }
        public override List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportModes mode)
        {
            NotifyObservers(fromCity, toCity, mode);
            var citiesBetween = FindCitiesBetween(fromCity, toCity);
            if (citiesBetween == null || citiesBetween.Count < 1 || routes == null || routes.Count < 1)
                return null;

            var source = citiesBetween[0];
            var target = citiesBetween[citiesBetween.Count - 1];

            Dictionary<City, double> dist;
            Dictionary<City, City> previous;
            var q = FillListOfNodes(citiesBetween, out dist, out previous);
            dist[source] = 0.0;

            // the actual algorithm
            previous = SearchShortestPath(mode, q, dist, previous);

            // create a list with all cities on the route
            var citiesOnRoute = GetCitiesOnRoute(source, target, previous);

            // prepare final list of links
            return FindPath(citiesOnRoute, mode);
        }

        private static List<City> FillListOfNodes(List<City> cities, out Dictionary<City, double> dist, out Dictionary<City, City> previous)
        {
            var q = new List<City>(); // the set of all nodes (cities) in Graph ;
            dist = new Dictionary<City, double>();
            previous = new Dictionary<City, City>();
            foreach (var v in cities)
            {
                dist[v] = double.MaxValue;
                previous[v] = null;
                q.Add(v);
            }

            return q;
        }

        /// <summary>
        /// Searches the shortest path for cities and the given links
        /// </summary>
        /// <param name="mode">transportation mode</param>
        /// <param name="q"></param>
        /// <param name="dist"></param>
        /// <param name="previous"></param>
        /// <returns></returns>
        private Dictionary<City, City> SearchShortestPath(TransportModes mode, List<City> q, Dictionary<City, double> dist, Dictionary<City, City> previous)
        {
            while (q.Count > 0)
            {
                City u = null;
                var minDist = double.MaxValue;
                // find city u with smallest dist
                foreach (var c in q)
                    if (dist[c] < minDist)
                    {
                        u = c;
                        minDist = dist[c];
                    }

                if (u != null)
                {
                    q.Remove(u);
                    foreach (var n in FindNeighbours(u, mode))
                    {
                        var l = FindLink(u, n, mode);
                        var d = dist[u];
                        if (l != null)
                            d += l.Distance;
                        else
                            d += double.MaxValue;

                        if (dist.ContainsKey(n) && d < dist[n])
                        {
                            dist[n] = d;
                            previous[n] = u;
                        }
                    }
                }
                else
                    break;

            }

            return previous;
        }


        /// <summary>
        /// Finds all neighbor cities of a city. 
        /// </summary>
        /// <param name="city">source city</param>
        /// <param name="mode">transportation mode</param>
        /// <returns>list of neighbor cities</returns>
        private List<City> FindNeighbours(City city, TransportModes mode)
        {
            var neighbors = new List<City>();
            /*diese LINQ expression ist keine vereinfachung des untenstehenden Codes
             * neighbors = routes.Where(r => r.TransportMode == mode && (r.FromCity.Equals(city) || r.ToCity.Equals(city)))
                .Select(c => c.FromCity).Concat(routes.Where(r => r.TransportMode == mode && (r.FromCity.Equals(city) || r.ToCity.Equals(city)))
                .Select(c => c.ToCity)).Where(c => !c.Equals(city)).Distinct().ToList();*/

            foreach (var r in routes)
                if (mode.Equals(r.TransportMode))
                {
                    if (city.Equals(r.FromCity))
                        neighbors.Add(r.ToCity);
                    else if (city.Equals(r.ToCity))
                        neighbors.Add(r.FromCity);
                }

            return neighbors;
        }

        private List<City> GetCitiesOnRoute(City source, City target, Dictionary<City, City> previous)
        {
            var citiesOnRoute = new List<City>();
            var cr = target;
            while (previous[cr] != null)
            {
                citiesOnRoute.Add(cr);
                cr = previous[cr];
            }
            citiesOnRoute.Add(source);

            citiesOnRoute.Reverse();
            return citiesOnRoute;
        }

        public Link FindLink(City fromCity, City toCity, TransportModes modes)
        {
            return routes.Where(l => (l.FromCity.Equals(fromCity) && l.ToCity.Equals(toCity) && l.TransportMode == modes) ||
                (l.FromCity.Equals(toCity) && l.ToCity.Equals(fromCity) && l.TransportMode == modes)).First();

            /*foreach (Link l in routes)
            {
                if (l.FromCity.Equals(fromCity) && l.ToCity.Equals(toCity) && l.TransportMode.Equals(modes))
                {
                    return l;
                }
                if (l.FromCity.Equals(toCity) && l.ToCity.Equals(fromCity) && l.TransportMode.Equals(modes))
                {
                    return l;
                }
            }
            return null;*/
        }

        public List<Link> FindPath(List<City> banane, TransportModes modes)
        {
            List<Link> output = new List<Link>();
            for (int i = 0; i < (banane.Count - 1); i++)
            {
                output.Add(FindLink(banane[i], banane[i + 1], modes));
            }
            return output;
        }

        public City[] FindCities(TransportModes transportMode)
        {
            return routes.Where(r => r.TransportMode == transportMode)
                .Select(r => r.ToCity).Concat(routes.Where(r => r.TransportMode == transportMode).Select(r => r.FromCity))
                .Distinct().ToArray();
        }
    }
}
