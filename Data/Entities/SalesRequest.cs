﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class SalesRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime RequestDate { get; set; }
        public int Status { get; set; } = 0;
        public string? Comment { get; set; }
        public int? MangerApproved { get; set; }
        [Required]
        public int StatioId { get; set; }
        [ForeignKey("StatioId")]
        public Station Station { get; set; }

    }
}
