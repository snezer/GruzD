using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GruzD.DAL.PgSql.Mapping
{
    public class SupplyTransportMapping : IEntityTypeConfiguration<SupplierTransport>
    {
        public void Configure(EntityTypeBuilder<SupplierTransport> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);

            builder.Property(t => t.InitialWeight);
            builder.Property(t => t.Number);
            builder.Property(t => t.RemainingWeight);
        }
    }
}
