using BL.IServices;
using Data.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Rest.Voice.V1.DialingPermissions.Country;

namespace Aman_gas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {

        /// <summary>
        /// DailyStationsReport
        /// DailySalesManReport
        /// MonthlyStationsReport
        /// MonthlySalesManReport
        /// </summary>
        private readonly IReportService RS; 
        public ReportController(IReportService RS)
        {
            this.RS = RS;
        }

        [HttpGet("DailyStationsReport")]
        public async Task<ActionResult<List<StationPointDailyTracker_V>>> DailyStationsReportAsync(int StationId = 0, string DateFrom="*" , string DateTo = "*" , int Step = 0 , int Take = 0 , string Search = "*") => Ok(await RS.StationDailyReportAsync(DateFrom,DateTo,StationId , Step , Take , Search));
        [HttpGet("DailySalesManReport")]
        public async Task<ActionResult<List<SalesManPointDailyTracker_V>>> DailySalesManReportAsync(int SalesManId = 0, string DateFrom = "*", string DateTo = "*", int Step = 0 , int Take = 0, string Search = "*") => Ok(await RS.SalesManDailyReportAsync(DateFrom,DateTo,SalesManId, Step, Take, Search));
         
        [HttpGet("MonthlyStationsReport")]
        public async Task<ActionResult<List<StationPointMonthlyTracker_V>>> MonthlyStationsReportAsync(int RegionId = 0, int StationId = 0,int Step = 0 , int Take = 0, string Search = "*") => Ok(await RS.StationMonthlyReportAsync(RegionId , StationId, Step, Take, Search));
        [HttpGet("MonthlySalesManReport")]
        public async Task<ActionResult<List<SalesManPointMonthlyTracker_V>>> MonthlySalesManReportAsync(int StationId = 0, int SalesManId = 0, int Step = 0, int Take = 0, string Search = "*") => Ok(await RS.SalesManMonthlyReportAsync(StationId , SalesManId,Step,Take,Search));
    }
}
