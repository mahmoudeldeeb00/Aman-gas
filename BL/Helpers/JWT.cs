﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class JWT
    {
        public string Key { get; set; }
        public string Isseur { get; set; }
        public string Audience { get; set; }
        public double ExpireInDays { get; set; }
    }
}
