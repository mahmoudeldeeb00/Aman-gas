using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOS
{
    public class UserDataDTO
    {

        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Gmail { get; set; }
        public decimal PointCredit { get; set; }
        public decimal PointDebit { get; set; }
        public decimal PointBalance { get; set; }
        public string Car { get; set; }
    }
}
