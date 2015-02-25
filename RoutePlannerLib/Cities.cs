using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Cities
{
    class Cities
    {
        List<City> cities = new List<City>();

        public int ReadCities(string filename)
        {
            TextReader reader = new StreamReader(filename);
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] cityArray = line.Split((char)9);
                cities.Add(new City(cityArray[0], cityArray[1], cityArray[2], cityArray[3], cityArray[4]));
                line = reader.ReadLine();
            }
            return 0;
        }

        public int Count
        {
            get { return cities.Count; }
        }

        public City Iterator(int index)
        {
            if (index < 0 || index >= cities.Count)
            {
                return null;
            }
            return cities[index];   
        }
    }
}
