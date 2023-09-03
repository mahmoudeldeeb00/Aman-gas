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

        /// <summary>
        /// //////Views 
        /// </summary>
        public virtual DbSet<TestView> TestViews { get; set; }
        public virtual DbSet<StationPointMonthlyTracker_V> StationPointMonthlyTracker_V { get; set; }
        public virtual DbSet<SalesManPointMonthlyTracker_V> SalesManPointMonthlyTracker_V { get; set; }
        public virtual DbSet<CarUsersBalance_V> CarUsersBalance_V { get; set; }
        public virtual DbSet<SalesManPointDailyTracker_V> SalesManPointDailyTracker_V { get; set; }
        public virtual DbSet<StationPointDailyTracker_V> StationPointDailyTracker_V { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            base.OnModelCreating(builder);
            builder.Entity<AssignPoints>()
                     .HasOne(u =>u.Fueling)
                     .WithMany()
                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SalesMan>()
                .HasOne(s => s.Station)
                .WithMany()
                .HasForeignKey(s=>s.StationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Fueling>()
                .HasOne(s => s.SalesMan)
                .WithMany()
                .HasForeignKey(s=>s.SalesManId)
                .OnDelete( DeleteBehavior.Restrict);
            builder.Entity<Fueling>()
                .HasOne(s=>s.Station)
                .WithMany()
                .HasForeignKey(s=>s.StationId)
                .OnDelete( DeleteBehavior.Restrict);

            builder.Entity<TestView>(a =>
           {
               a.HasNoKey();
               a.ToView("TestView");
           });  
            builder.Entity<CarUsersBalance_V>(a =>
           {
               a.HasNoKey();
               a.ToView("CarUsersBalance_V");
           });  
            builder.Entity<SalesManPointMonthlyTracker_V>(a =>
           {
               a.HasNoKey();
               a.ToView("SalesManPointMonthlyTracker_V");
           });
            builder.Entity<StationPointMonthlyTracker_V>(a =>
           {
               a.HasNoKey();
               a.ToView("StationPointMonthlyTracker_V");
           });
            builder.Entity<SalesManPointDailyTracker_V>(a =>
           {
               a.HasNoKey();
               a.ToView("SalesManPointDailyTracker_V");
           });
            builder.Entity<StationPointDailyTracker_V>(a =>
           {
               a.HasNoKey();
               a.ToView("StationPointDailyTracker_V");
           });
                
        }

    }
}
