using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GruzD.DAL.PgSql.Mapping
{
    public class CompanyTransportMapping:IEntityTypeConfiguration<CompanyTransport>
    {
        public void Configure(EntityTypeBuilder<CompanyTransport> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);

            builder.Property(t => t.CurrentWeight);
            builder.Property(t => t.Number);
        }
    }
}
