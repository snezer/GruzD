using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GruzD.DAL.PgSql.Mapping
{
    public class UnloadingZonesMapping:IEntityTypeConfiguration<UnloadingZone>
    {
        public void Configure(EntityTypeBuilder<UnloadingZone> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CurrentWeight);
            builder.Property(t => t.Name);
            builder.HasMany(t => t.Sources).WithOne(s => s.SupplyZone).HasForeignKey(s => s.UnloadingZoneId);
        }
    }
}
