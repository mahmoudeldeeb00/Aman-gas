using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public  class AddSalesRequestDTO
    {
   
        [Required]
        public string Name { get; set; }
        public string Password { get; set; }
        public string? Comment { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public int StationId { get; set; }
        [Required ,MinLength(14),MaxLength(14)]
        public string? NationalId { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
    }
}
