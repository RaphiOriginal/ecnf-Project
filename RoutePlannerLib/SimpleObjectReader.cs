using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new object();
        }
    }
}
