using BL.DTOS;
using BL.Helpers;
using BL.UOW;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IStationService 
    {
       public Task <Response<string>> AddStationAsync(AddStationDTO dto );
    }
}
