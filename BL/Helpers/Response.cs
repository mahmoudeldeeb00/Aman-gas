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
    public class PagResponse<T> : Response<T> where T : class
    {
        public int Count { get; set; }
        public int Pages { get; set; }
    }

    public static class HandleException <T>where T:class
    {
       public static Response<T> Handle(Exception ex)
        {
            if (ex.Message.Contains("An error occurred while saving the entity changes"))
                return new Response<T>() { State = 10, Data = null, ErrorMessage = "Foreign Key Error !!" }; 
            if (ex.Message.Contains("Object reference not set to an instance of an object"))
                return new Response<T>() { State = 10, Data = null, ErrorMessage = "Null Reference Error !!" };
            return new Response<T>() { State = 10 , Data = null ,Message = "Error" ,  ErrorMessage = ex.Message };
         } 
        public static PagResponse<T> PagHandle(Exception ex)
        {
            if (ex.Message.Contains("An error occurred while saving the entity changes"))
                return new PagResponse<T>() { State = 10, Data = null, ErrorMessage = "Foreign Key Error !!" };
            if (ex.Message.Contains("Object reference not set to an instance of an object"))
                return new PagResponse<T>() { State = 10, Data = null, ErrorMessage = "Null Reference Error !!" };
            return new PagResponse<T>() { State = 10 , Data = null ,Message = "Error" ,  ErrorMessage = ex.Message };
         } 
    }
}
