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
    public class FuelingDTO
    {
        // Fueling 
        [Required]
        public string SalesManIdOrName { get; set; }
        [Required]
        public string SalesManPassword { get; set; }
        [Required]
        public int FuelTypeId { get; set; }
        [Required]
        public decimal FuelSize { get; set; }
        [Required]
        public int StationId { get; set; }
        public int FuelingType { get; set; }

    }
}
