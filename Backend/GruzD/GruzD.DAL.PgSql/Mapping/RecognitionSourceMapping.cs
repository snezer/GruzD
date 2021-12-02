using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace GruzD.DAL.PgSql.Mapping
{
    public class RecognitionSourceMapping : IEntityTypeConfiguration<RecognitionSource>
    {
        public void Configure(EntityTypeBuilder<RecognitionSource> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);

            builder.Property(t => t.CameraName);
            builder.Property(t => t.Description);
            builder.Property(t => t.ProvidingType);
            builder.HasOne(t => t.SupplyZone).WithMany(z => z.Sources).HasForeignKey(t => t.UnloadingZoneId);
        }
    }
}
