using Microsoft.EntityFrameworkCore;
using Web_Api_Colleg_App.Data.Config;
using Web_Api_Colleg_App.Models;

namespace Web_Api_Colleg_App.Data;

public class CollegeAppDbContext : DbContext
{
	public CollegeAppDbContext(DbContextOptions<CollegeAppDbContext> options)
		:base(options)
	{
	}

	public DbSet<Student> Students { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfig());
        base.OnModelCreating(modelBuilder);
    }
}

