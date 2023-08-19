using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class AddStationDTO
    {
        [Required]
        public string Name { get; set; }
        public string ARName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [Required]
        public int RegionId { get; set; }
        public decimal Longtude { get; set; }
        public decimal Latitude { get; set; }




    }
}
