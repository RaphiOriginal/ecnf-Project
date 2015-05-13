using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fhnw.Ecnf.RoutePlanner.RoutePlannerLib.Util
{
    public class SimpleObjectWriter
    {
        StringWriter stream;
        public SimpleObjectWriter(StringWriter _stream)
        {
            stream = _stream;
        }
        public void Next(Object next)
        {
            Type type = next.GetType();
            PropertyInfo[] propertys = type.GetProperties();
            stream.Write("Instance of "+type.FullName+"\r\n");
            foreach (PropertyInfo p in propertys.Where(e => e.CanRead))
            {
                if (p.GetCustomAttributes().Count() == 0)
                {
                    if (p.GetValue(next).GetType() == typeof(string))
                    {
                        stream.Write(p.Name + "=\"" + p.GetValue(next) + "\"\r\n");
                    }
                    else if (p.GetValue(next).GetType() == typeof(double))
                    {
                        string temp = "" + ((double)p.GetValue(next)).ToString(CultureInfo.InvariantCulture);
                        temp = temp.Replace(',', '.');
                        stream.Write(p.Name + "=" + temp + "\r\n");
                    } 
                    else if(p.GetValue(next).GetType() == typeof(int))
                    {
                        string temp = "" + ((int)p.GetValue(next)).ToString(CultureInfo.InvariantCulture);
                        stream.Write(p.Name + "=" + temp + "\r\n");
                    }
                    else if (p.GetValue(next).GetType() == typeof(bool))
                    {
                        stream.Write(p.Name + "=" + p.GetValue(next) + "\r\n");
                    }
                    else
                    {
                        stream.Write(p.Name + " is a nested object...\r\n");
                        Next(p.GetValue(next));
                    }
                }
            }
            stream.Write("End of instance\r\n");
        }
    }
}
