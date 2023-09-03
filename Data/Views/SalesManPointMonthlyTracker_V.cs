using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class SalesManPointMonthlyTracker_V
    {
 				
        public int SalesManId { get; set; }
        public string Name { get; set; }
        public string Month { get; set; }
        public decimal CreditPoints { get; set; }
        public decimal DebitPoints { get; set; }
    }
}
