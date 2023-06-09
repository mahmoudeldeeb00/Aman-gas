﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class getTokenModel
    {
        [Required(ErrorMessage ="UserName is Required ")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is Required ")]
        public string Password { get; set; }
    }
}
