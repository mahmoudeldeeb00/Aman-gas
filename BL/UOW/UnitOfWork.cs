using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContainer _context;

        public UnitOfWork(DbContainer context)
        {
            _context = context;
            this.Ranges = new BaseRepo<Data.Entities.Range>(_context);
            this.Regions = new BaseRepo<Region>(_context);
            this.Stations = new BaseRepo<Station>(_context);
            this.salesMen = new BaseRepo<SalesMan>(_context);
            this.Fuelings = new BaseRepo<Fueling>(_context);
            this.Cars = new BaseRepo<Car>(_context);
            this.Assignpoints = new BaseRepo<AssignPoints>(_context);
        }

        public IBaseRepo<Data.Entities.Range> Ranges { get; private set; }
        public IBaseRepo<Region> Regions { get; private set; }

        public IBaseRepo<Station> Stations { get; private set; }

        public IBaseRepo<SalesMan> salesMen { get; private set; }

        public IBaseRepo<Fueling> Fuelings { get; private set; }

        public IBaseRepo<Car> Cars { get; private set; }

        public IBaseRepo<AssignPoints> Assignpoints { get; private set; }
        public IBaseRepo<SalesRequest> SalesRequests { get; private set; }

       
        public int Complete()=> _context.SaveChanges();
        public void Dispose() { _context.Dispose(); }
        
    }
}
