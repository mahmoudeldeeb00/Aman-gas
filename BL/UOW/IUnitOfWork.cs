using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
namespace BL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepo<Data.Entities.Range> Ranges { get; }
        IBaseRepo<Region> Regions { get; }
        IBaseRepo<Station> Stations { get; }
        IBaseRepo<SalesMan> salesMen { get; }
        IBaseRepo<Fueling> Fuelings { get; }
        IBaseRepo<Car> Cars { get; }
        IBaseRepo<AssignPoints> Assignpoints { get; }
        IBaseRepo<SalesRequest> SalesRequests { get; }


        int Complete();
    }
}
