using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Data.Entities;
using Data.Views;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork UOW;
        private readonly UserManager<AppUser> USM;
        public UserService(IUnitOfWork UOW , UserManager<AppUser> USM)
        {
            this.UOW = UOW;
            this.USM = USM;
        }
        public async Task<Response<UserDataDTO>> GetUserDataAsync(string Username)
        {
            try
            {
                var CurrentUser = await USM.FindByNameAsync(Username);
                Car UserCar = await UOW.Cars.FindAsync(s => s.User == Username , new string[1]{ "CarType"});
                CarUsersBalance_V Us = await UOW.CarUsersBalance_V.FindAsync(s => s.UserId == CurrentUser.Id); 

                UserDataDTO dto = new UserDataDTO
                {
                    UserName = Username,
                    FirstName = CurrentUser.FirstName,
                    Id = CurrentUser.Id,
                    Gmail = CurrentUser.Gmail,
                    LastName = CurrentUser.LastName,
                    Car = Us.Car,
                    PointBalance = Us.Balance,
                    PointCredit = Us.Credit,
                    PointDebit = Us.Debit

                };
                return new Response<UserDataDTO> { State = 1, Data = dto, Message = "Current User Data " };
            }catch(Exception ex)
            { return HandleException<UserDataDTO>.Handle(ex); }
        }
    }
}
