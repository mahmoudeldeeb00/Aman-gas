using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Data.Entities;
using Data.Views;
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
                // Fueling Type 1 => دا هيفول يدفع فلوس عادي وياخد نقطة 
                // Fueling Type 2 => دا هيفول بالنقط بتاعته  

                model.SalesManPassword = Encrypt_Decrypt.Encrypt(model.SalesManPassword);
                SalesMan Sales =await UOW.SalesMen.FindAsync(s=>s.Name == model.SalesManIdOrName && s.Status == 1 && s.Password == model.SalesManPassword);
                if (Sales is null) { try
                    {
                        Sales = await UOW.SalesMen.FindAsync(s => s.Id == Int32.Parse(model.SalesManIdOrName) && s.Status == 1 && s.Password == model.SalesManPassword);
                    }
                    catch { }
                }
                if (Sales is null)
                    return new Response<string> { State = 2, Message = "Invalid Sales Man Data " };

                if(Sales.StationId != model.StationId)
                    return new Response<string> { State = 2, Message = "Sales Man IS Not Work In This Station " };
                
                if(model.FuelingType!=1 && model.FuelingType != 2)
                    return new Response<string> { State = 2, Message = "Invalid Fueling Type Earn Or Cashing " };
                

                DateTime NNow = AmanGasTime.Now();
                Fueling CurrentFueling = new Fueling
                {
                    Date = NNow,
                    FuelSize = model.FuelSize,
                    FuelTypeId = model.FuelTypeId,
                    SalesManId = Sales.Id,
                    StationId = model.StationId,
                    Status = model.FuelingType,
                    UserId = FuelingUser
                };
                await UOW.Fuelings.AddAsync(CurrentFueling);
                UOW.Complete();
                FuelType FUELTYPE = await UOW.FuelTypes.FindAsync(s => s.Id == model.FuelTypeId, new string[1] { "UnitType" });
                decimal FuelMoney = model.FuelSize * FUELTYPE.Price;               
                int FuelingId = CurrentFueling.Id;
                PointsRatio PR = await UOW.PointsRatios.FindAsync(s => s.FueltTypeId == model.FuelTypeId);// النسب 
                try // This try To Disable Fueling in above Lines If Assign Points Error Ocuur
                 {
                    if (CurrentFueling.Status == 1)// fuel and gain points 
                    {
                        
                        decimal points = 0; // Setting To Choose Gain Points Based On Fuel Size Or Based On Money
                        if(FUELTYPE.AssignPointBasedOn.ToUpper()=="MONEY")
                            points = FuelMoney * PR.MoneyRatio;
                        if (FUELTYPE.AssignPointBasedOn.ToUpper() == "SIZE")
                            points = model.FuelSize * PR.Ratio; 
                        AssignPoints AP = new AssignPoints
                        {
                            Count = points,Date = NNow,FuelingId = FuelingId,
                            Status = model.FuelingType, UserId = FuelingUser
                        };
                        await UOW.Assignpoints.AddAsync(AP);
                        UOW.Complete();
                        string Message = $" Success Operation ; {Math.Round(points, 2)} Point Have Been Earned due To Fueling {model.FuelSize} {FUELTYPE.UnitType.Name} as {FuelMoney} L.E of {FUELTYPE.Name}";
                        CarUsersBalance_V PointBalance = await UOW.CarUsersBalance_V.FindAsync(s => s.UserId == FuelingUser);
                        if (PointBalance is not null)
                            Message += " And New Balance is : " + PointBalance.Balance;  
                        
                        return new Response<string> { State = 1, Data = points.ToString(), Message = Message };

                    }
                    else // 2 // Fueling By His Points 
                    {
                        decimal points = 0;
                        if (FUELTYPE.AssignPointBasedOn.ToUpper() == "MONEY")
                            points = FuelMoney * PR.MoneyRatio;
                        if (FUELTYPE.AssignPointBasedOn.ToUpper() == "SIZE")
                            points = model.FuelSize * PR.Ratio;

                        CarUsersBalance_V CurrentPointBalance = await UOW.CarUsersBalance_V.FindAsync(s => s.UserId == FuelingUser);
                        if (CurrentPointBalance is not null)
                        {
                          
                            if (CurrentPointBalance.Balance < points ) // Compare between Fueling Balance And User Point Balance 
                            {
                                CurrentFueling.Status = 0;
                                CurrentFueling.VoidingDescription = "User Point Balance is Less Than Fueling Balance";
                                UOW.Fuelings.Update(CurrentFueling);
                                UOW.Complete();
                                return new Response<string> { State = 2, Data = "", Message = $"User Point {CurrentPointBalance.Balance} Is Less Than Fueling point {points} "  };

                            }
                        }
                        
                        AssignPoints AP = new AssignPoints {
                            Count = points,
                            Date = NNow,
                            FuelingId = FuelingId,
                            Status = model.FuelingType,
                            UserId = FuelingUser
                        };
                        await UOW.Assignpoints.AddAsync(AP);
                        UOW.Complete();

                        string Message = $" Success Operation ; {Math.Round(points, 2)} Point Have Been CASHED due To Fueling {model.FuelSize} {FUELTYPE.UnitType.Name} as {FuelMoney} L.E of {FUELTYPE.Name}";
                        CarUsersBalance_V PointBalance = await UOW.CarUsersBalance_V.FindAsync(s => s.UserId == FuelingUser);
                        if (PointBalance is not null)
                            Message += " And New Balance is : " + PointBalance.Balance;
                        return new Response<string> { State = 1, Data = points.ToString(), Message = Message };
                      
                    }
                    

                }
                catch (Exception ex)
                    {
                        UOW.RollBack();
                        CurrentFueling.Status = 0;
                    CurrentFueling.VoidingDescription = "Error When Saving Points ";
                    UOW.Fuelings.Update(CurrentFueling);
                        UOW.Complete();

                    return HandleException<string>.Handle(ex);

                 }



              



            }
            catch (Exception ex)
            {
                return HandleException<string>.Handle(ex);
            }


        }



       
    }
}
