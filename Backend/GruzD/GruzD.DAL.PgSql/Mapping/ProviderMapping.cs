using System;
using System.Collections.Generic;
using System.Text;
using GruzD.DataModel.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace GruzD.DAL.PgSql.Mapping
{
    public class ProviderMapping:IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Account);
            builder.Property(t => t.BIK);
            builder.Property(t => t.CEO);
            builder.Property(t => t.CorrAccount);
            builder.Property(t => t.Email);
            builder.Property(t => t.FactAddress);
            builder.Property(t => t.INN);
            builder.Property(t => t.KPP);
            builder.Property(t => t.LegalAddress);
            builder.Property(t => t.Name);
            builder.Property(t => t.Phone);
            builder.Property(t => t.Site);
            builder.HasMany(t => t.Supplies).WithOne(e => e.Provider).HasForeignKey(e => e.ProviderId);
        }
    }
}
