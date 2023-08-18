using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class RegisterModel
    {
        public char[]? CarChars { get; set; }
        public string CarNumbers { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }

        public string UserName { get; set; }
      
        public string Email { get; set; }
        [Required]
        public string Pasword { get; set; }
        public string? Gmail { get; set; }
        public string? PhoneNumber { get; set; }
        public int CarType { get; set; } = 0;
    }
}
