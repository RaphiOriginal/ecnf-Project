using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set;}

        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name;
            Latitude = _latitude;
            Longitude = _longitude;
        }

      
        public override string ToString() {
             string text;
             if (this.Name != null || this.Name != "")
             {
                 text = string.Format("WayPoint: {0} {1:N2}/{2:N2}", Name, Latitude, Longitude);
             }
             else
             {
                text = string.Format("WayPoint: {0f2}/{1f2}", Latitude, Longitude);
             }
             return text;
        }

        public double Distance(WayPoint target)
        {
            double faktor = Math.PI / 180;
            const double r = 6371;
            return r * Math.Acos(Math.Sin(Latitude * faktor) * Math.Sin(target.Latitude * faktor) 
                + Math.Cos(Latitude * faktor) * Math.Cos(target.Latitude * faktor) * Math.Cos(Latitude * faktor - target.Latitude * faktor));
        }
    }
}
