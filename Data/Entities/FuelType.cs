using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class FuelType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ARName { get; set; }
        public int UnitTypeId { get; set; }
        [ForeignKey("UnitTypeId")]
        public UnitType UnitType { get; set; }
        [Column(TypeName = "decimal(10,4)")]
        public decimal Price  { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public DateTime  Date { get; set; } 


    }
}
