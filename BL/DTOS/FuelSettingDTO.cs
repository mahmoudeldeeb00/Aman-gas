using Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class FuelSettingDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ARName { get; set; }
        [Required]
        public decimal Ratio { get; set; }
        [Required]
        public int UnitTypeId { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; } = 1;
        public string? AssignPointBasedOn { get; set; } = "MONEY";
    }
}
