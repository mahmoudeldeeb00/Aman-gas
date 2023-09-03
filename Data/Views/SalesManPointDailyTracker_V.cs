﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class SalesManPointDailyTracker_V
    {
        public int SalesManId { get; set; }
        public string Name { get; set; }
        public DateTime Day { get; set; }
        public string DayInString { get; set; }
        public decimal CreditPoints { get; set; }
        public decimal DebitPoints { get; set; }
    }
}
