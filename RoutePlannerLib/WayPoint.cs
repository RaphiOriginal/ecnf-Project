﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class WayPoint
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public WayPoint() { }
        public WayPoint(string _name, double _latitude, double _longitude)
        {
            Name = _name;
            Latitude = _latitude;
            Longitude = _longitude;
        }

      
        public override string ToString() {
             string text;
             if (!string.IsNullOrEmpty(Name))
             {
                 text = string.Format("WayPoint: {0} {1:N2}/{2:N2}", Name, Latitude, Longitude);
             }
             else
             {
                text = string.Format("WayPoint: {0:N2}/{1:N2}", Latitude, Longitude);
             }
             return text;
        }

        public double Distance(WayPoint target)
        {
            double faktor = Math.PI / 180;
            const double r = 6371.0;
            return r * Math.Acos(Math.Sin(Latitude * faktor) * Math.Sin(target.Latitude * faktor)
                + Math.Cos(Latitude * faktor) * Math.Cos(target.Latitude * faktor) * Math.Cos(Longitude * faktor - target.Longitude * faktor));
        }

        public static WayPoint operator +(WayPoint links, WayPoint rechts)
        {
            return new WayPoint(links.Name, links.Latitude + rechts.Latitude, links.Longitude + rechts.Longitude);
        }

        public static WayPoint operator -(WayPoint links, WayPoint rechts)
        {
            return new WayPoint(links.Name, links.Latitude - rechts.Latitude, links.Longitude - rechts.Longitude);
        }
    }
}
