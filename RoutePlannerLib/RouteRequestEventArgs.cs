using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RouteRequestEventArgs : System.EventArgs
    {
        public string FromCity
        {
            get;
            set;
        }
        public string ToCity
        {
            get;
            set;
        }
        public TransportModes Transport
        {
            get;
            set;
        }
        public RouteRequestEventArgs(string _fromCity, string _toCity, TransportModes _transport)
        {
            this.FromCity = _fromCity;
            this.ToCity = _toCity;
            this.Transport = _transport;
        }
    }
}
