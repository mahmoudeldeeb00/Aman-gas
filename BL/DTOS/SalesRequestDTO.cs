using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public  class SalesRequestDTO
    {
     //  SalesRequest
          public int Id { get; set; }
        public string Name { get; set; }
       // public string Password { get; set; }
        public DateTime RequestDate { get; set; }
       // public int Status { get; set; } = 0;
        public string? Comment { get; set; }
      //  public string MangerApproved { get; set; }
        public int StatioId { get; set; }
        public string StationName { get; set; }
        
    }
}
