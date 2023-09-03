using BL.Helpers;
using BL.IServices;
using BL.UOW;
using Data.Entities;
using Data.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Twilio.Base;

namespace BL.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork UOW;
        public ReportService(IUnitOfWork UOW)
        {
            this.UOW = UOW;
        }

        public async Task<PagResponse<List<SalesManPointDailyTracker_V>>> SalesManDailyReportAsync(string DateFrom , string DateTo , int SalesManId, int Step, int Take, string Search)
        {
            if(DateFrom =="*" && DateTo == "*")
            {

                DateFrom = "2000/01/01";DateTo = "2300/01/01";
            }
            DateTime dfrom = DateTime.Parse(DateFrom);
            DateTime dto = DateTime.Parse(DateTo);
            PagResponse<List<SalesManPointDailyTracker_V>> Result = new PagResponse<List<SalesManPointDailyTracker_V>>();
          
            try
            {
                var data = await UOW.SalesManPointDailyTracker_V.FindAllAsync(s => s.Day >=dfrom && s.Day <= dto);
                if(SalesManId != 0)
                        data = data.Where(s => SalesManId == SalesManId);
                if (Search != "*")
                    data = data.Where(s => s.Name.Contains(Search) || s.DayInString.Contains(Search));
                
                Result.State = 1;
                Result.Count = data.Count();
                if(Take != 0 && Step != 0)
                {

                Result.Pages = Int32.Parse(Math.Ceiling(Decimal.Parse(Result.Count.ToString()) / Decimal.Parse(Take.ToString())).ToString());
                Result.Message = "Sales Man Daily Report ";
                 Result.Data = data.OrderByDescending(s => s.Day).Skip((Step-1) * Take).Take(Take).ToList();
                }
                else
                {
                    Result.Data = data.OrderByDescending(s => s.Day).ToList();
                }
                
                    return Result;
             
            }
            catch(Exception ex) {
                return HandleException<List<SalesManPointDailyTracker_V>>.PagHandle(ex); }
         
        }

        public async Task<PagResponse<List<SalesManPointMonthlyTracker_V>>> SalesManMonthlyReportAsync(int StationId , int SalesManId, int Step, int Take, string Search)
        {
            try
            {
                PagResponse<List<SalesManPointMonthlyTracker_V>> Response = new PagResponse<List<SalesManPointMonthlyTracker_V>>();
                    var data = await UOW.SalesManPointMonthlyTracker_V.GetAllAsync();
                if (SalesManId != 0)
                    data = data.Where(s => s.SalesManId == SalesManId);
              
                if(StationId != 0)
                {
                    List<int> MANS = UOW.SalesMen.FindAllAsync(S => S.StationId == StationId).Result.Select(s => s.Id).ToList();
                    data = data.Where(s => MANS.Contains(s.SalesManId));
                }
                if (Search != "*")
                    data = data.Where(s => s.Name.Contains(Search) || s.Month.Contains(Search));

                Response.State = 1;
                Response.Count = data.Count();
                if (Take != 0 && Step != 0)
                {

                    Response.Pages = Int32.Parse(Math.Ceiling(Decimal.Parse(Response.Count.ToString()) / Decimal.Parse(Take.ToString())).ToString());
                    Response.Message = "Sales Man Monthly Report ";
                    Response.Data = data.OrderByDescending(s => s.Month).Skip((Step - 1) * Take).Take(Take).ToList();
                }
                else
                {
                    Response.Data = data.OrderByDescending(s => s.Month).ToList();
                }

                return Response;

            }
            catch(Exception ex)
            { return HandleException<List<SalesManPointMonthlyTracker_V>>.PagHandle(ex); }
        }

        public async Task<PagResponse<List<StationPointDailyTracker_V>>> StationDailyReportAsync(string DateFrom, string DateTo, int StationId, int Step, int Take, string Search)
        {
            if (DateFrom == "*" && DateTo == "*")
            {
                DateFrom = "2000/01/01"; DateTo = "2300/01/01";
            }
            DateTime dfrom = DateTime.Parse(DateFrom);
            DateTime dto = DateTime.Parse(DateTo);
            PagResponse<List<StationPointDailyTracker_V>> Result = new PagResponse<List<StationPointDailyTracker_V>>();

            try
            {
                var data = await UOW.StationPointDailyTracker_V.FindAllAsync(s => s.Day >= dfrom && s.Day <= dto);
                if (StationId != 0)
                    data = data.Where(s => s.StationId == StationId);

                if (Search != "*")
                    data = data.Where(s => s.Name.Contains(Search) || s.DayInString.Contains(Search));

                Result.State = 1;
                Result.Count = data.Count();
                if (Take != 0 && Step != 0)
                {
                    Result.Pages = Int32.Parse(Math.Ceiling(Decimal.Parse(Result.Count.ToString()) / Decimal.Parse(Take.ToString())).ToString());
                    Result.Message = "Station Daily Report ";
                    Result.Data = data.OrderByDescending(s => s.Day).Skip((Step - 1) * Take).Take(Take).ToList();
                }
                else
                {
                    Result.Data = data.OrderByDescending(s => s.Day).ToList();
                }

                return Result;
              }
            catch (Exception ex)
            { return HandleException<List<StationPointDailyTracker_V>>.PagHandle(ex); }
        }

        public async Task<PagResponse<List<StationPointMonthlyTracker_V>>> StationMonthlyReportAsync(int RegionId , int StationId, int Step, int Take, string Search)
        {
            try
            {
                PagResponse<List<StationPointMonthlyTracker_V>> Response = new PagResponse<List<StationPointMonthlyTracker_V>>();
                  var data = await UOW.StationPointMonthlyTracker_V.GetAllAsync();
                if (StationId != 0)
                    data = data.Where(s => s.StationId == StationId);
                if( RegionId != 0)
                {
                    List<int> stations = UOW.Stations.FindAllAsync(s=>s.RegionId == RegionId).Result.Select(s=>s.Id).ToList();
                    data = data.Where(s => stations.Contains(s.StationId));
                }

                if (Search != "*")
                    data = data.Where(s => s.Name.Contains(Search) || s.Month.Contains(Search));
                Response.State = 1;
                Response.Count = data.Count();
                if (Take != 0 && Step != 0)
                {

                    Response.Pages = Int32.Parse(Math.Ceiling(Decimal.Parse(Response.Count.ToString()) / Decimal.Parse(Take.ToString())).ToString());
                    Response.Message = "Station Monthly Report ";
                    Response.Data = data.OrderByDescending(s => s.Month).Skip((Step - 1) * Take).Take(Take).ToList();
                }
                else
                {
                    Response.Data = data.OrderByDescending(s => s.Month).ToList();
                }

                return Response;
            }
            catch (Exception ex)
            {return HandleException<List<StationPointMonthlyTracker_V>>.PagHandle(ex);}
        }
    }
}
