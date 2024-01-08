using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public static class AmanGasTime
    {
        public static DateTime Time(DateTime time) => time.AddHours(8);
        public static DateTime Now() =>Time(DateTime.Now);
        public static string _Now() =>Time(DateTime.Now).ToString("yyyy-MMM-dd hh:mm tt");
    }
}
