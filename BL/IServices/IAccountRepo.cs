using BL.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IAccountRepo
    {
        Task<AuthenticationModel> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> TokenAsync(getTokenModel model);
        Task<string> AddNewRoleAsync(string role);
    }
}
