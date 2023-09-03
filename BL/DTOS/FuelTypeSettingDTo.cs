using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class FuelTypeSettingDTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ARName { get; set; }
        public string? AssignPointBasedOn { get; set; } = "Money";
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public decimal Ratio { get; set; }
        
    }
}
