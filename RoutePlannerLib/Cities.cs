using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class Cities
    {
        public List<City> cities = new List<City>();

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
           int counter = 0;
           using (TextReader reader = new StreamReader(filename))
           {
               IEnumerable<string[]> citiesAsStrings = reader.GetSplittedLines('\t');

               foreach (string[] cs in citiesAsStrings)
               {
                   cities.Add(new City(cs[0].Trim(), cs[1].Trim(), int.Parse(cs[2], CultureInfo.InvariantCulture), 
                       double.Parse(cs[3], CultureInfo.InvariantCulture), double.Parse(cs[4], CultureInfo.InvariantCulture)));
                   counter ++;
               }
           }
            return counter;

        }

        public int Count
        {
            get { return cities.Count; }
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            SortedDictionary<double, City> list = new SortedDictionary<double, City>();
            List<City> neigbours = new List<City>();
            foreach(City city in cities){
                var result = location.Distance(city.Location);
                if (result <= distance)
                {
                    list.Add(result, city);
                }
            }
            foreach (KeyValuePair<double, City> city in list)
            {
                neigbours.Add(city.Value);
            }
            return neigbours;
        }
        public City FindCity(string cityName)
        {
            return FindCityName(delegate(City c)
            {
                return cityName.Equals(c.Name, StringComparison.InvariantCultureIgnoreCase);
            });
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
