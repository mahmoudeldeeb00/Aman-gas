using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class FuelingService : IFuelingService
    {
        private readonly IUnitOfWork UOW; 
        public FuelingService(IUnitOfWork UOW)
        {
            this.UOW = UOW;
        }
        public async Task<Response<string>> AddFueling(FuelingDTO model ,string FuelingUser)
        {
            try
            {
               
                model.SalesManPassword = Encrypt_Decrypt.Encrypt(model.SalesManPassword);
                SalesMan Sales =await UOW.SalesMen.FindAsync(s=>s.Name == model.SalesManIdOrName && s.Status == 1);
                if (Sales is null) { try
                    {
                        Sales = await UOW.SalesMen.FindAsync(s => s.Id == Int32.Parse(model.SalesManIdOrName) && s.Status == 1);
                    }
                    catch { }
                }
                if (Sales is null)
                    return new Response<string> { State = 2, Message = "Invalid Sales Man Data " };

                if(Sales.StationId != model.StationId)
                    return new Response<string> { State = 2, Message = "Sales Man IS Not Work In This Station " };
                DateTime NNow = AmanGasTime.Now();
                Fueling CurrentFueling = new Fueling
                {
                    Date = NNow,
                    FuelSize = model.FuelSize,
                    FuelTypeId = model.FuelTypeId,
                    SalesManId = Sales.Id,
                    StationId = model.StationId,
                    Status = 1,
                    UserId = FuelingUser
                };
                await UOW.Fuelings.AddAsync(CurrentFueling);
                UOW.Complete();
                int FuelingId = CurrentFueling.Id;
                try{
                    /// النسب 
                    PointsRatio PR = await UOW.PointsRatios.FindAsync(s => s.FueltTypeId == model.FuelTypeId );
                    FuelType FUELTYPE =await UOW.FuelTypes.FindAsync(s => s.Id == model.FuelTypeId, new string[1] { "UnitType" });
                    decimal points = model.FuelSize * PR.Ratio;
                    AssignPoints AP = new AssignPoints
                    {
                        Count = points,
                        Date = NNow,
                       FuelingId = FuelingId,
                       
                        Status = 1,
                        UserId = FuelingUser
                    };
                    await UOW.Assignpoints.AddAsync(AP);
                    UOW.Complete();
                    string Message = $" Success Operation ; \n {Math.Round(points,2)} Point Have Been Earned due To Fueling {model.FuelSize } {FUELTYPE.UnitType.Name} of {FUELTYPE.Name}";
                    return new Response<string> { State = 1,Data=points.ToString() ,  Message = Message };


                }
                catch(Exception ex)
                {
                    UOW.RollBack();
                    CurrentFueling.Status = 0;
                    UOW.Fuelings.Update(CurrentFueling);
                    UOW.Complete();

                    if (ex.Message.Contains("An error occurred while saving the entity changes"))
                    {
                        return new Response<string>() { State = 10, Data = null, ErrorMessage = "Error Loss Of Data Entered NUll Or Foreign Keys" };
                    }
                    return new Response<string> {State = 500 , Message = "Error ! ", ErrorMessage = ex.Message };
                    
                }
                
                
              




            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("An error occurred while saving the entity changes"))
                {
                    return new Response<string>() { State = 10, Data = null, ErrorMessage = "Error Loss Of Data Entered NUll Or Foreign Keys" };
                }
                return new Response<string> {State = 500 , Message = "Error ! ", ErrorMessage = ex.Message };
            }


        }

    }
}
