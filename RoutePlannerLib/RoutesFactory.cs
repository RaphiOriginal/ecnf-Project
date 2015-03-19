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
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type aClass = assembly.GetType(algorithmClassName);
            
            if(aClass == null || !aClass.IsClass){
                return null;
            }

            Type[] citiesType = new Type[] { cities.GetType() };
            ConstructorInfo constructor = aClass.GetConstructor(citiesType);
            object[] parameter = new object[] { cities };
            object route = constructor.Invoke(parameter);
            return route as IRoutes;
        }
    }
}
