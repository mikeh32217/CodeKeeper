using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKeeper.Utilities
{
    public class DateTimeFormatter
    {
        public static string DateTimeFormat(string date)
        {
            DateTime dti = Convert.ToDateTime(date);
            string dts = dti.ToString("yyyy-MM-dd");

            return dts;
        }
    }
}
