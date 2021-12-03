using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;
using GruzD.DataModel.Events;
using Microsoft.EntityFrameworkCore;

namespace GruzD.DAL.PgSql
{
    public class LogicDataContext : DbContext
    {
        public LogicDataContext()
        {
        }

        public LogicDataContext(DbContextOptions<LogicDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Mapping.ProcessEventMapping().Configure(modelBuilder.Entity<ProcessEvent>());
            new Mapping.RecognitionSourceMapping().Configure(modelBuilder.Entity<RecognitionSource>());
            new Mapping.UnloadingZonesMapping().Configure(modelBuilder.Entity<UnloadingZone>());
            new Mapping.ProviderMapping().Configure(modelBuilder.Entity<Provider>());
            new Mapping.SupplyMapping().Configure(modelBuilder.Entity<Supply>());
            new Mapping.RawMaterialMapping().Configure(modelBuilder.Entity<RawMaterialType>());
            new Mapping.CompanyTransportMapping().Configure(modelBuilder.Entity<CompanyTransport>());
            new Mapping.SupplyTransportMapping().Configure(modelBuilder.Entity<SupplierTransport>());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BLOBData> Blobs { get; set; }
        public DbSet<ProcessEvent> ProcessEvents { get; set; }
        public DbSet<RecognitionSource> RecognitionSources { get; set; }
        public DbSet<UnloadingZone> UnloadingZones { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplierTransport> SupplierTransports { get; set; }
        public DbSet<CompanyTransport> CompanyTransports { get; set; }
        public DbSet<RawMaterialType> RawMaterialTypes { get; set; }
    }
}
