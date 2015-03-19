using RoutePlannerLib.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib
{
    public class RoutesFactory
    {
        static public IRoutes Create(Cities cities)
        {
            var setting = Settings.Default.RouteAlgorithm;
            return Create(cities, setting);
            
        }
        static public IRoutes Create(Cities cities, string algorithmClassName)
        {
            Assembly assembly = Assembly.LoadFrom(algorithmClassName);
            /*Type types = assembly.GetType("IRoutes");
            if(types == null || !types.IsClass){
                return null;
            }*/
            object routeClass = assembly.CreateInstance(algorithmClassName);
            IRoutes route = routeClass as IRoutes;
            return route;
        }
    }
}
