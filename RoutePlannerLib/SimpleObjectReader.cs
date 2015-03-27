using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectReader
    {
        StringReader toRead;
        public SimpleObjectReader(StringReader _toRead)
        {
            toRead = _toRead;
        }
        public object Next()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return nextObject(assembly);
        }
        private object nextObject(Assembly assembly)
        {
            object temp = null;
            string currentLine;
            while ((currentLine = toRead.ReadLine()) != null)
            {
                string[] workStrings = currentLine.Split(' ');
                if (workStrings[0].Equals("Instance"))
                {
                    Type aClass = assembly.GetType(workStrings[2]);
                    temp = assembly.CreateInstance(workStrings[2]);
                }
                else if (workStrings.Length == 1)
                {
                    PropertyInfo[] propertys = temp.GetType().GetProperties();
                    string[] keyValueString = currentLine.Split('=');
                    if (keyValueString[1].Contains("\""))
                    {
                        keyValueString[1] = keyValueString[1].Remove(0, 1);
                        keyValueString[1] = keyValueString[1].Remove(keyValueString[1].Length - 1, 1);
                        foreach (PropertyInfo f in propertys)
                        {
                            if (f.Name.Equals(keyValueString[0]))
                            {
                                f.SetValue(temp, keyValueString[1]);
                            }
                        }
                    }
                    else if (keyValueString[1].Contains("."))
                    {
                        foreach (PropertyInfo f in propertys)
                        {
                            if (f.Name.Equals(keyValueString[0]))
                            {
                                f.SetValue(temp, double.Parse(keyValueString[1], CultureInfo.InvariantCulture));
                            }
                        }
                    }
                    else
                    {
                        foreach (PropertyInfo f in propertys)
                        {
                            if (f.Name.Equals(keyValueString[0]))
                            {
                                f.SetValue(temp, int.Parse(keyValueString[1], CultureInfo.InvariantCulture));
                            }
                        }
                    }
                }
                else if (workStrings.Length == 5)
                {
                    PropertyInfo[] fields = temp.GetType().GetProperties();
                    foreach (PropertyInfo f in fields)
                    {
                        if (f.Name.Equals(workStrings[0]))
                        {
                            object child = nextObject(assembly);
                            f.SetValue(temp, child);
                        }
                    }
                }
                else if (workStrings[0].Equals("End"))
                {
                    return temp;
                }
            }
            return null;
        }
    }
}