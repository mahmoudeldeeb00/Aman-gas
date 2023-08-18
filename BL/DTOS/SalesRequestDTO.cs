using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public  class SalesRequestDTO
    {
        [Required(ErrorMessage ="Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string? Password { get; set; }

        public int StationId { get; set; }
    }
}
