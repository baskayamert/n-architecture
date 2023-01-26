using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class HospitalEntityConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name).IsRequired().HasMaxLength(30);

            builder.Property(h => h.Lat).IsRequired().HasMaxLength(15);
            builder.Property(h => h.Long).IsRequired().HasMaxLength(15);

            builder.Property(h => h.Address).IsRequired().HasMaxLength(80);


        }
    }
}
