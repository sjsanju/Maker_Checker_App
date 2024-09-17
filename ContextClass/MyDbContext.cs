using DotNetCoreMVC_MakerChecker.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreMVC_MakerChecker.ContextClass
{
    public class MyDbContext : DbContext
    {       
            public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
            {
            }

            public DbSet<Employee> TBL_CKR_Employees { get; set; }
            public DbSet<Login> Logins { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Employee>()
                    .HasKey(e => e.EmployeeId);

                modelBuilder.Entity<Employee>()
                    .Property(e => e.EmployeeId)
                    .ValueGeneratedOnAdd();

                modelBuilder.Entity<Login>()
                    .HasKey(l => l.UserId);
            }
        }    
}
