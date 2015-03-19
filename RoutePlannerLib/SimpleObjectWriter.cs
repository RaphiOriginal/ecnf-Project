using System;
using System.Collections.Generic;
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
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] propertys = type.GetProperties();
            stream.Write("Instance of "+type.FullName+"\r\n");
            foreach (FieldInfo f in fields)
            {
                if (f.GetValue(next).GetType() == typeof(string))
                {
                    stream.Write(f.Name + "=\"" + f.GetValue(next) + "\"\r\n");
                }
                else
                {
                    stream.Write(f.Name + "=" + f.GetValue(next) + "\r\n");
                }
            }
            foreach (PropertyInfo p in propertys)
            {
                stream.Write(p.Name + " is a nested object...\r\n");
                Next(p);
            }
            stream.Write("End of instance\r\n");
        }
    }
}
