using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GruzD.DAL.PgSql.Mapping
{
    public class RawMaterialMapping : IEntityTypeConfiguration<RawMaterialType>
    {
        public void Configure(EntityTypeBuilder<RawMaterialType> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Density);
            builder.Property(t => t.Name);
        }
    }
}
