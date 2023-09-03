using AutoMapper;
using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class StationService : IStationService
    {
        private readonly IUnitOfWork UOF;
        private readonly IMapper Mapper ; 
        public StationService(IUnitOfWork UOF , IMapper map)
        {
            this.UOF = UOF;
            this.Mapper = map;
        }
        public async Task<Response<string>> AddStationAsync(AddStationDTO dto)
        {
            try
            {
                var entity = Mapper.Map<Station>(dto);
                entity.DateCreated = DateTime.Now;
               Station s =  await  UOF.Stations.AddAsync(entity);
                if (UOF.Complete() > 0)
                    return new Response<string> { State = 1, Data = s.Id.ToString(), Message = "Station Added Successfully ! " };
                return new Response<string> { State =2, Message = "Station Not Added Successfully ! " };

            }catch(Exception ex) { return HandleException<string>.Handle(ex); }

           
        }
    }


}
