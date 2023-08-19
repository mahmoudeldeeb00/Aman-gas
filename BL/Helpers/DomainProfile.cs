using AutoMapper;
using BL.DTOS;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class DomainProfile : Profile
    {
        public DomainProfile( )
        {
           CreateMap<Station , AddStationDTO> (); 
           CreateMap<AddStationDTO, Station> ();


            CreateMap<SalesRequest, SalesRequestDTO>();
            CreateMap<SalesRequestDTO, SalesRequest>();

            CreateMap<SalesRequest , AddSalesRequestDTO> (); 
           CreateMap<AddSalesRequestDTO, SalesRequest> ();


            //CreateMap<List<SalesRequest>, List<SalesRequestDTO>>();
            //CreateMap< List<SalesRequestDTO>, List<SalesRequest>>();


        }
    }
}
