using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Views
{
    [Keyless]
    public class TestView
    {
        public string? Name { get; set; }
        public string? User { get; set; }
        public string? CarTypeAN { get; set; }
    }
}
