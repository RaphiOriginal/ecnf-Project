﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TextReader reader = new StreamReader(filename);
            string line = reader.ReadLine();
            int counter = 0;
            while (line != null)
            {
                counter++;
                string[] cityArray = line.Split((char)9);
                cities.Add(new City(cityArray[0], cityArray[1], int.Parse(cityArray[2]), Double.Parse(cityArray[3]), Double.Parse(cityArray[4])));
                line = reader.ReadLine();
            }
            return counter;
        }

        public int Count
        {
            get { return cities.Count; }
        }

        public List<City> FindNeighbours(WayPoint location, double distance)
        {
            List<City> neigbours = new List<City>();
            foreach(City city in cities){
                if (location.Distance(city.Location) <= distance && location.Distance(city.Location) != 0)
                {
                    neigbours.Add(city);
                }
            }
            return neigbours;
        }
    }
}