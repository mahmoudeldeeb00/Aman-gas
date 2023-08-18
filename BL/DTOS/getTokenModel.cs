using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class getTokenModel
    {
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is Required ")]
        public string Password { get; set; }
        public char[]? CarChars { get; set; }

        public string? CarNumbers { get; set; }
    }
}
