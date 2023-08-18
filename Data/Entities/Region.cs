using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Region
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ARName { get; set; }

        public string? Description { get; set; }
        public int Status { get; set; }
        public int RangeId { get; set; }
        [ForeignKey("RangeId")]
        public Range Range { get; set; }
    }
}
