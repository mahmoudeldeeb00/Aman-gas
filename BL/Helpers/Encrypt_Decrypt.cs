using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public static class Encrypt_Decrypt
    {
        public static string Encrypt (string Value)=> Convert.ToBase64String(Encoding.UTF8.GetBytes(Value));
           
        public static string Decrypt (string Value)=>Encoding.UTF8.GetString(Convert.FromBase64String(Value));
    }
}
