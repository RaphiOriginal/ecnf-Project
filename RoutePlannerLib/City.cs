using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class City
    {
        [XmlIgnore]
        public int Index { get; set; }

        public string Name
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public int Population
        {
            get;
            set;
        }

        public WayPoint Location
        {
            get;
            set;
        }

        public City() { }

        public City(string _name, string _country, int _population, double _longitude, double _latitude) {
            Name = _name;
            Country = _country;
            Population = _population;
            Location = new WayPoint(_name, _longitude, _latitude);
        }

        public override bool Equals(object o)
        {
            if (o is City)
            {
                var r = (City) o;
                if (this.Name.Equals(r.Name, StringComparison.InvariantCultureIgnoreCase) && this.Country.Equals(r.Country, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;

        }
    }
}
