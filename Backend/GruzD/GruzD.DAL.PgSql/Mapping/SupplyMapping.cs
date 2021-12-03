using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GruzD.DAL.PgSql.Mapping
{
    public class SupplyMapping : IEntityTypeConfiguration<Supply>
    {
        public void Configure(EntityTypeBuilder<Supply> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FactDate);
            builder.Property(t => t.PlannedDate);
            builder.HasOne(t => t.Provider).WithMany(e => e.Supplies).HasForeignKey(t => t.ProviderId);
            builder.HasOne(t => t.RawMaterialType).WithMany().HasForeignKey(t => t.ProviderId);
        }
    }
}
