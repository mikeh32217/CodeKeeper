using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeKeeper.Utilities
{
    public static class NormalizeText
    {
        /// <summary>
        /// Removes the quotes.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <returns></returns>
        public static string Normalize(string src)
        {
            Regex r1 = new Regex("\'");
            Regex r2 = new Regex("\"");

            src = r1.Replace(src, "''");
            src = r2.Replace(src, @"""");

            return src;
        }
    }
}
