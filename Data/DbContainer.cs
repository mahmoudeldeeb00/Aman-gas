using Data.Entities;
using Data.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DbContainer : IdentityDbContext<AppUser>
    {
        public DbContainer()
        {
        }

        public DbContainer(DbContextOptions<DbContainer> opts) : base(opts) { }

        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Entities.Range> Ranges { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<AssignPoints> AssignPointss { get; set; }
        public DbSet<Fueling> Fuelings { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<PointsRatio> PointsRatios { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<SalesMan> SalesMen { get; set; }
        public DbSet<SalesRequest> SalesRequest { get; set; }


        public virtual DbSet<TestView> TestViews { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            base.OnModelCreating(builder);
            builder.Entity<AssignPoints>()
                     .HasOne(u =>u.Fueling)
                     .WithMany()
                     .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<SalesMan>()
                .HasOne(s => s.Station)
                .WithMany();

            builder.Entity<TestView>(a =>
           {
               a.HasNoKey();
               a.ToView("TestView");
           });
                
        }

    }
}
