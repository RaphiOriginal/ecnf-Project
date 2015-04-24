using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        public List<City> cities = new List<City>();
        TraceSource traceSource = new TraceSource("CitiesTrace");

        public City this[int index]
        {
            get {
                if (index < 0 || index >= cities.Count)
                {
                    return null;
                }
                return this.cities[index];  }
            set { this.cities[index] = value; }
        }

        public int ReadCities(string filename)
        {
           //traceSource.TraceInformation("ReadCities started!");
           int counter = cities.Count;
            try
            {
                using (TextReader reader = new StreamReader(filename))
                {
                    IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

                    var citiesTemp =
                        citiesAsStrings.Select(
                            cs => new City(cs[0].Trim(), cs[1].Trim(), int.Parse(cs[2], CultureInfo.InvariantCulture),
                                double.Parse(cs[3], CultureInfo.InvariantCulture),
                                double.Parse(cs[4], CultureInfo.InvariantCulture)));
                    cities.AddRange(citiesTemp);
                    //traceSource.TraceInformation("ReadCities ended!");
                    return cities.Count - counter;
                }
            }
            catch (FileNotFoundException e)
            {
                //traceSource.TraceEvent(TraceEventType.Critical, 1, "404 File not Found!");
                return 0;
            }
        }

        public int Count
        {
            get { return cities.Count; }
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {

            return cities.Where(c => location.Distance(c.Location) <= distance)
                .OrderBy(c => location.Distance(c.Location)).ToList();
        }
        public City FindCity(string cityName)
        {
            return FindCityName(c => cityName.Equals(c.Name, StringComparison.InvariantCultureIgnoreCase));
           
        }
        public City FindCityName(Predicate<City> pred)
        {
            foreach (City city in cities)
            {
                if (pred(city))
                {
                    return city;
                }
            }
            return null;
        }

        public List<City> FindCitiesBetween(City from, City to)
        {
            var foundCities = new List<City>();
            if (from == null || to == null)
                return foundCities;

            foundCities.Add(from);

            var minLat = Math.Min(from.Location.Latitude, to.Location.Latitude);
            var maxLat = Math.Max(from.Location.Latitude, to.Location.Latitude);
            var minLon = Math.Min(from.Location.Longitude, to.Location.Longitude);
            var maxLon = Math.Max(from.Location.Longitude, to.Location.Longitude);

            // rename the name of the "cities" variable to your name of the internal City-List
            foundCities.AddRange(cities.FindAll(c => c.Location.Latitude > minLat && 
                    c.Location.Latitude < maxLat && c.Location.Longitude > minLon && 
                    c.Location.Longitude < maxLon));

            foundCities.Add(to);
            return foundCities;
        }
    }
}
