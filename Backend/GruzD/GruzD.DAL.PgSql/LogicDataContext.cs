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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new Mapping.ProcessEventMapping().Configure(modelBuilder.Entity<ProcessEvent>());
            new Mapping.RecognitionSourceMapping().Configure(modelBuilder.Entity<RecognitionSource>());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BLOBData> Blobs { get; set; }
        public DbSet<ProcessEvent> ProcessEvents { get; set; }
        public DbSet<RecognitionSource> RecognitionSources { get; set; }
        public DbSet<UnloadingZone> UnloadingZones { get; set; }
    }
}
