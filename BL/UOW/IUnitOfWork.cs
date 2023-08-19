using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.IServices;
using Data.Entities;
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


        int Complete();
        void RollBack();
    }
}
