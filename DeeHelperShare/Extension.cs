using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeeHelper
{
    public static class Extension
    {
        public static bool Contains(this string str, List<string> strings)
        {
            return strings.Any(st => str.Contains(st));
        }

        public static bool SqlContains(this string str, string containString)
        {
            if (String.IsNullOrEmpty(containString)) return false;
            if (containString.First() == '%' && containString.Last() == '%')
            {
                containString = containString.Remove(0, 1);
                containString = containString.Remove(containString.Length - 1, 1);
                return str.Contains(containString);
            }
            else
            {
                string strTemp = str;
                if (containString.First() == '%')
                {
                    containString = containString.Remove(0, 1);
                    int indexOfContainString = str.IndexOf(containString);
                    if (indexOfContainString != 0 && indexOfContainString > 0)
                    {
                        strTemp = str.Remove(0, indexOfContainString);
                    }
                }
                else if (containString.Last() == '%')
                {
                    containString = containString.Remove(containString.Length - 1, 1);
                    int indexOfContainString = str.LastIndexOf(containString);
                    if (str.Substring(indexOfContainString).Length > containString.Length && indexOfContainString > 0)
                    {
                        strTemp = str.Remove(indexOfContainString + containString.Length, str.Length - indexOfContainString - containString.Length);
                    }
                }
                return strTemp == containString;
            }
        }
    }
}
