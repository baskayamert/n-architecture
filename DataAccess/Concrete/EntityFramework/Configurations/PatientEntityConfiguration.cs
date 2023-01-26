using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Configurations
{
    public class PatientEntityConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(80);

            builder.Property(p => p.HospitalId).IsRequired();

            builder.Property(p => p.NationalityNumber).IsRequired().HasMaxLength(20);
            builder.HasIndex(p => p.NationalityNumber).IsUnique();

            builder.HasOne(p => p.Hospital).WithMany(h => h.Patients).HasForeignKey(p => p.HospitalId).OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.Property(p => p.LatestVisitedDepartment).HasMaxLength(20);


        }
    }
}
