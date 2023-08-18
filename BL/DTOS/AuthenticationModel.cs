﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class AuthenticationModel
    {
        public string Message { get; set; }
        public char[]? GetChars { get; set; }
        public string? CarNumbers { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
