using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PointsRatio
    {
        [Key]
        public int Id { get; set; }
        public int FueltTypeId { get; set; }
        [ForeignKey("FuelTypeId")]
        public FuelType FuelType { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Ratio { get; set; } 
        public decimal MoneyRatio { get; set; }

    }
}
