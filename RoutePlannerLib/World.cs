using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Dynamic
{
    public class World : DynamicObject
    {
        Cities cities;
        public World(Cities _cities)
        {
            cities = _cities;
        }
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object city)
        {
            City exists = cities.FindCity(binder.Name);
            if (exists != null)
            {
                city = exists;
                return true;
            }
            else
            {
                city = "The city \"" + binder.Name + "\" does not exist!";
                return true;
            }
        }
    }
}
