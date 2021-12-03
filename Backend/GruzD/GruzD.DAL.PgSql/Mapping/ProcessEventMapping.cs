using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace GruzD.DAL.PgSql.Mapping
{
    public class ProcessEventMapping : IEntityTypeConfiguration<ProcessEvent>
    {
        public void Configure(EntityTypeBuilder<ProcessEvent> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);
            builder.Property(t => t.AuxData);
            builder.Property(t => t.ClassifiedType);
            
            builder.Property(t => t.Occured);
            builder.Property(t => t.Registered);
            builder.HasMany(t => t.Sources).WithOne(t => t.ProcessEvent).HasForeignKey(s => s.ProcessEventId);

            builder.Property(t => t.Number);
            builder.Property(t => t.Weight);

            builder.Property(t => t.ProcessTime);
            builder.Property(t => t.Processed);

            builder.HasOne(t => t.UnloadingZone).WithMany().HasForeignKey(t => t.UnloadingZoneId);
        }
    }
}
