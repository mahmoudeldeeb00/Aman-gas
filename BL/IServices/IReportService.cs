using BL.Helpers;
using Data.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IServices
{
    public interface IReportService
    {

        Task<PagResponse<List<SalesManPointMonthlyTracker_V>>> SalesManMonthlyReportAsync(int StationId , int SalesManId , int Step , int Take , string Search);
        Task<PagResponse<List<StationPointMonthlyTracker_V>>> StationMonthlyReportAsync(int RedionId , int StationId, int Step, int Take, string Search); 
        Task<PagResponse<List<SalesManPointDailyTracker_V>>> SalesManDailyReportAsync(string DateFrom ,string DateTo  , int SalesManId, int Step, int Take, string Search);
        Task<PagResponse<List<StationPointDailyTracker_V>>> StationDailyReportAsync(string DateFrom , string DateTo , int StationId, int Step, int Take, string Search);
    }
}
