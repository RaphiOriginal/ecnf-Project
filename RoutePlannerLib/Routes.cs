
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    /// <summary>
    /// Manages a routes from a city to another city.
    /// </summary>
    public abstract class Routes : IRoutes
    {
        protected List<Link> routes = new List<Link>();
        protected Cities cities;
        protected TraceSource traceSource = new TraceSource("RoutesTrace");

        public int Count
        {
            get { return routes.Count; }
        }
        public bool ExecuteParallel { set; get; }

        /// <summary>
        /// Initializes the Routes with the cities.
        /// </summary>
        /// <param name="cities"></param>
        public Routes(Cities cities)
        {
            this.cities = cities;
        }
        public Routes() { }

        /// <summary>
        /// Reads a list of links from the given file.
        /// Reads only links where the cities exist.
        /// </summary>
        /// <param name="filename">name of links file</param>
        /// <returns>number of read route</returns>
        public int ReadRoutes(string filename)
        {
            traceSource.TraceInformation("ReadRoutes started!");
            using (TextReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var linkAsString = line.Split('\t');

                    City city1 = cities.FindCity(linkAsString[0]);
                    City city2 = cities.FindCity(linkAsString[1]);

                    // only add links, where the cities are found 
                    if ((city1 != null) && (city2 != null))
                    {
                        routes.Add(new Link(city1, city2, city1.Location.Distance(city2.Location),
                                                   TransportModes.Rail));
                    }
                }
            }
            traceSource.TraceInformation("ReadRoutes ended!");
            return Count;

        }
        public List<City> FindCitiesBetween(string from, string to)
        {
            return cities.FindCitiesBetween(cities.FindCity(from), cities.FindCity(to));
        }
        public abstract List<Link> FindShortestRouteBetween(string fromCity, string toCity, TransportModes mode);
       
        public delegate void RouteRequestHandler(object sender, RouteRequestEventArgs e);
        public event RouteRequestHandler RouteRequestEvent;

        public void NotifyObservers(string fromCity, string toCity, TransportModes mode)
        {
            if (RouteRequestEvent != null)
            {
                RouteRequestEvent(this, new RouteRequestEventArgs(fromCity, toCity, mode));
            }
        }
    }
}
