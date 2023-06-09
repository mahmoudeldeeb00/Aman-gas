using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        public string? Name{ get; set; }
        
        public int CarTypeId{ get; set; }
        [ForeignKey("CarTypeId")]    
        public CarType CarType { get; set; } 
        public string UserId{ get; set; }
        [ForeignKey("UserId")]    
        public AppUser AppUser { get; set; } 
    }
}
