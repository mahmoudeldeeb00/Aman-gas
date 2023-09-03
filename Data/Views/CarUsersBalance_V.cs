using Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    public class CarUsersBalance_V
    {
                 
            public string UserId { get; set; }
            public string Name { get; set; }
            public string Car { get; set; }
            public decimal Credit { get; set; }
            public decimal Debit { get; set; }
            public decimal Balance { get; set; }
    }
}
