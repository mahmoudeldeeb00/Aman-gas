using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AssignPoints
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
        public int FuelingId { get; set; }
        [ForeignKey("FuelingId")]
        public Fueling Fueling { get; set; }
        public DateTime?  Date { get; set; }
        public int Status { get; set; }
        [Column(TypeName ="decimal(10,4)")]
        public decimal Count { get; set; }

    }
}
