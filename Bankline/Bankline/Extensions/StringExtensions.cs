using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Extensions
{
    public static class StringExtensions
    {
        public static bool OpenTag(this string line, string propertyName)
        {
            return line.Contains($"<{propertyName}>");
        }
        public static bool ClosedTag(this string line, string propertyName)
        {
            return line.Contains($"</{propertyName}>");
        }
    }
}
