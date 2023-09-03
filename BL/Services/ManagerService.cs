using AutoMapper;
using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork UOW; 
        private readonly IMapper Mapper; 
        public ManagerService(IUnitOfWork UOW , IMapper Mapper)
        {
            this.UOW = UOW;    
            this.Mapper = Mapper;
        }

        public async Task<Response<string>> AcceptSalesRequestsAsync(int RequestId, int Deny, string Editor = null)
        {
                SalesRequest request = await UOW.SalesRequests.FindAsync(s => s.Id == RequestId);
            try
            {
                if (request is null)
                    return new Response<string>() { State = 2, Data = null, Message = "No Request Found with This ID " };
                if (request.Status == 1)
                    return new Response<string>() { State = 3, Data = null, Message = "This Request Have Been Approved from While " };
                if (request.Status == 2)
                    return new Response<string>() { State = 3, Data = null, Message = "Your Request Have Been Denied !! " };

                if (Deny == 0) /// Aprove 
                {

                    SalesMan Sale = new SalesMan()
                    {
                        Name = request.Name,
                        JoinDate = DateTime.Now,
                        DateOfBirth = request.DateOfBirth,
                        Password = request.Password,
                        StationId = request.StationId,
                        Status = 1,
                        NationalId = request.NationalId,
                        PhoneNumber = request.PhoneNumber

                    };

                    await UOW.SalesMen.AddAsync(Sale);
                    request.Status = 1;
                    request.MangerApproved = Editor;
                    UOW.SalesRequests.Update(request);

                    UOW.Complete();
                    return new Response<string>() { State = 1, Message = "Sales Request Approved Succefully !" };
                }
                /// Denying 
                request.Status = 2;// DENIED
                request.MangerApproved = Editor;
                UOW.SalesRequests.Update(request);
                UOW.Complete();
                return new Response<string>() { State = 1, Message = "Sales Request Denied Succefully !" };
            }catch(Exception ex)
            {
                return HandleException<string>.Handle(ex);
            }
        }

        public async Task<Response<FuelSettingDTO>> GetFuelSettingAsync(int FuelId)
        {
            try{
                PointsRatio Entity = await UOW.PointsRatios.FindAsync(f => f.FueltTypeId == FuelId, new string[] { "FuelType" });
                FuelSettingDTO Model = new FuelSettingDTO
                {
                    Id = Entity.FueltTypeId,
                    Name = Entity.FuelType.Name,
                    ARName = Entity.FuelType.ARName,
                    AssignPointBasedOn = Entity.FuelType.AssignPointBasedOn,
                    Description = Entity.FuelType.Description,
                    Price = Entity.FuelType.Price,
                    UnitTypeId = Entity.FuelType.UnitTypeId
                };

                Model.Ratio = Model.AssignPointBasedOn.ToUpper() == "MONEY" ? Math.Round(1 / Entity.MoneyRatio , 0) : Math.Round(1 / Entity.Ratio,0);
                return new Response<FuelSettingDTO> { State = 1, Message = Model.Name + " Setting ", Data = Model }; 


            }catch(Exception ex)
            {
                return HandleException<FuelSettingDTO>.Handle(ex);
            }
        }

        public async Task<Response<List<SalesRequestDTO>>> PendingSalesRequestsAsync()
        {
            try
            {

                string[] Includes = { "Station" };
                var requests = await UOW.SalesRequests.FindAllAsync(f => f.Status == 0, Includes);
                if (requests is not null)
                {
                    List<SalesRequestDTO> models = Mapper.Map<List<SalesRequestDTO>>(requests.ToList());
                    models = models.Select(s => { s.StationName = requests.FirstOrDefault(f => f.Id == s.Id).Station.Name; return s; }).ToList();
                    return new Response<List<SalesRequestDTO>> { State = 1, Data = models, Message = "Pending Sales Men Requests " };
                }
                return new Response<List<SalesRequestDTO>> { State = 2, Data = new List<SalesRequestDTO>(), Message = "No Pending Sales Men Requests Found  !" };
            }
            catch (Exception ex)
            {
              return HandleException<List<SalesRequestDTO>>.Handle(ex);
            }
        }

        public async Task<Response<string>> SetFuelSettingAsync(FuelSettingDTO DTO)
        {
            try
            {
                PointsRatio Entity = await UOW.PointsRatios.FindAsync(f => f.FueltTypeId == DTO.Id, new string[] { "FuelType" });
                Entity.FuelType.UnitTypeId = DTO.UnitTypeId;
                Entity.FuelType.Price = DTO.Price;
                Entity.FuelType.Status = DTO.Status;
                Entity.FuelType.ARName = DTO.ARName;
                Entity.FuelType.Name = DTO.Name;
                Entity.FuelType.AssignPointBasedOn = DTO.AssignPointBasedOn;
                Entity.FuelType.Description = DTO.Description;
                if (DTO.AssignPointBasedOn.ToUpper() == "MONEY")
                {
                    Entity.MoneyRatio = 1 / DTO.Ratio;
                }
                else
                {
                    Entity.Ratio = 1 / DTO.Ratio; 
                }
                UOW.PointsRatios.Update(Entity);
                UOW.Complete();
                return new Response<string> { State = 1, Message = DTO.Name + " Setting Updated Successfully ! " };
            }
            catch (Exception ex)
            {
                return HandleException<string>.Handle(ex);
            }
        }

        public async Task <Response<string>> TransfereSalesManAsync(int StationId, int SalesId)
        {
            try
            {
                SalesMan Sl = await UOW.SalesMen.FindAsync(f => f.Id == SalesId && f.Status == 1);
                if (Sl == null)
                    return new Response<string> { State = 2, Message = "No Sales Man With This id" };
               Sl.StationId = StationId;
               UOW.SalesMen.Update(Sl);
                UOW.Complete();
                    return new Response<string> { State = 1, Message = "Sales Man Transfered Succefully ! " };
               
            }
            catch(Exception ex) {
                return HandleException<string>.Handle(ex);
            }
        }





    }
}
