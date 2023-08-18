using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class SalesMan
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Status { get; set; }
        public DateTime? JoinDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int StationId { get; set; }
        [ForeignKey("StationId")]
        public Station Station { get; set; }


    }
}
