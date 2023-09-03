using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IServices;
using Data.Entities;
using Data.Views;
using Twilio.Jwt.AccessToken;

namespace BL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepo<Data.Entities.Range> Ranges { get; }
        IBaseRepo<Region> Regions { get; }
        IBaseRepo<SalesMan> SalesMen { get; }
        IBaseRepo<Fueling> Fuelings { get; }
        IBaseRepo<Car> Cars { get; }
        IBaseRepo<CarType> CarTypes { get; }
        IBaseRepo<UnitType> UnitTypes { get; }
        IBaseRepo<AssignPoints> Assignpoints { get; }
        IBaseRepo<SalesRequest> SalesRequests { get; }
        IBaseRepo<PointsRatio> PointsRatios { get; }
        IBaseRepo<Station> Stations { get; }
        IBaseRepo<FuelType> FuelTypes { get; }





        /// Views 
        IBaseRepo<CarUsersBalance_V> CarUsersBalance_V { get; }
        IBaseRepo<SalesManPointMonthlyTracker_V> SalesManPointMonthlyTracker_V { get; }
        IBaseRepo<StationPointMonthlyTracker_V> StationPointMonthlyTracker_V { get; }
        IBaseRepo<SalesManPointDailyTracker_V> SalesManPointDailyTracker_V { get; }
        IBaseRepo<StationPointDailyTracker_V> StationPointDailyTracker_V { get; }
      


        int Complete();
        void RollBack();
    }
}
