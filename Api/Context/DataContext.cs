using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CleaningSaboms.Context
{
    public class DataContext : IdentityDbContext<Models.ApplicationUser, Models.ApplicationRole, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<CustomerAddressEntity> CustomerAddresses { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<CustomerEntity>()
                .HasOne(c => c.CustomerAddress)
                .WithMany (a => a.Customers)
                .HasForeignKey(c => c.CustomerAddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
