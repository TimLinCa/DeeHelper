using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeeHelper
{
    public static class Extension
    {
        public static bool Contains(this string str,List<string> strings)
        {
            return strings.Any(st => str.Contains(st));
        }
    }
}
