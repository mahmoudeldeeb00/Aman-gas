using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class Response<T> where T : class
    {
        public int State { get; set; }
        public T? Data { get; set; }
        public string?  Message { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
