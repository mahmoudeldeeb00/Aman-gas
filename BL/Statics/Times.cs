using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Statics
{
    public static class Times
    {
        public static string Now() => DateTime.Now.ToString("yyyy/MMM/dd hh:mm tt ");
    }
}
