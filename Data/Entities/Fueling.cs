using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Fueling // تفويلة
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
        public DateTime Date { get; set; }
        public int FuelTypeId { get; set; }
        [ForeignKey("FuelTypeId")]
        public FuelType FuelType { get; set; }
        [Column(TypeName = "decimal(12,4)")]
        public decimal FuelSize { get; set; }
        public int StationId { get; set; }
        [ForeignKey("StationId")]
        public Station Station { get; set; }
        public int? Status { get; set; } 

    }
}
