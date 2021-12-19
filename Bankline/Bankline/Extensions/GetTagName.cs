using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Extensions
{
    public static class GetTag
    {
        public static string Name(string line)
        {
            try
            {
                return line.Substring(1, line.IndexOf(">") - 1);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
