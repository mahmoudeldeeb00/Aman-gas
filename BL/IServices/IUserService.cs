﻿using BL.DTOS;
using BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IUserService
    {
        Task<Response<UserDataDTO>> GetUserDataAsync(string Username);
    }
}
