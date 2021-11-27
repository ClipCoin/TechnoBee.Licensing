using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Technobee.Licensing.Storage.DataModels.Models;
using Technobee.Marketing.Core_Abstractions.Data;

namespace Technobee.Marketing.Licensing.DataModels.Data
{

    public partial class LicenseDbContext : CommonDbContext
    {
        public DbSet<Product> Product { get; set; }
        //public DbSet<Staff> Staff { get; set; }
        //public DbSet<Property> Properties { get; set; }
        //public DbSet<Person> Persons { get; set; }


        public LicenseDbContext()
            : base(@"Data Source=localhost\SQLEXPRESS02;Initial Catalog=ESLicensing;Integrated Security=True;Connection Timeout=30")
        {
           
        }

        //public ClientManagementDbContext(DbContextOptions<CommonDbContext> options)
        //    : base(options)
        //{ }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>().HasKey(p => p.ID);
            modelBuilder.Entity<Product>().Property(x => x.ID).HasColumnName("ProductID");

            // The Identity Key for the EmployeeId
            modelBuilder.Entity<Product>().Property(c => c.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Product>()
.Property(d => d.Guid).IsRequired();

            modelBuilder.Entity<Product>()
.Property(d => d.Name).IsRequired();

            modelBuilder.Entity<Product>()
.Property(d => d.Timestamp).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
