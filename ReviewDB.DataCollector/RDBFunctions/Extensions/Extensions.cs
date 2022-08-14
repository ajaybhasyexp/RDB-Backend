using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorFunctions.Extensions
{
    public static class Extensions
    {
        public static string FilePrefix(this DateTime date)
        {
            return $"{date.Month.ToString("00")}_{date.Day.ToString("00")}_{date.Year}";
        }
    }
}
