﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class RegisterModel
    {
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        [Required, StringLength(20)]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string Pasword { get; set; }
        public string? Gmail { get; set; }
    }
}
