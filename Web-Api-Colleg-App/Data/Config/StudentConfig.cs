using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Api_Colleg_App.Models;

namespace Web_Api_Colleg_App.Data.Config;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        //To Tabke
        builder.ToTable("Students");
        // Check for Keys
        builder.HasKey(x => x.StudentId);

        // Prop Validation
        builder.Property(x => x.StudentId).UseIdentityColumn();
        builder.Property(x => x.StudentName).IsRequired();
        builder.Property(x => x.StudentName).HasMaxLength(30);
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(200);
        builder.Property(x => x.Emial).IsRequired();
        builder.Property(x => x.Emial).HasMaxLength(50);

        
        builder.HasData(new List<Student>()
        {
            new Student()
            {
                StudentId = 1,
                StudentName = "Sharan",
                Address = "Koopal, KA",
                Emial = "Sharan@gmail.com"
            },
            new Student()
            {
                StudentId = 2,
                StudentName = "Shrikant",
                Address = "Athani, KA",
                Emial = "Shrikant@gmail.com"
            }
        });
    }
}

