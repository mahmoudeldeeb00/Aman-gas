using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ARName { get; set; }
        public string? Address { get; set; }
        [Column(TypeName = "decimal(11,8)")]

        public decimal Longtude { get; set; }
        [Column(TypeName = "decimal(11,8)")]

        public decimal Latitude { get; set; }
        public  int  RegionId { get; set; }
        [ForeignKey("RegionId")]
        public Region? Region { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateCreated { get; set; } 
    }
}
