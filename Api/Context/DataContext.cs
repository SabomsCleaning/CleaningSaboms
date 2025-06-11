using CleaningSaboms.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleaningSaboms.Context
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<CustomerAddressEntity> CustomerAddresses { get; set; } = null!;
        public DbSet<AuditLogger> AuditLogs { get; set; }
        public DbSet<BookingCleanerEntity> BookingCleaner { get; set; }
        public DbSet<BookingEntity> Booking { get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<CustomerEntity>()
                .HasOne(c => c.CustomerAddress)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.CustomerAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceType>()
                .Property(s => s.BasePrice)
                .HasPrecision(10, 2);

            builder.Entity<BookingEntity>(builder =>
                {
                    builder.Property(b => b.Id)
                    .HasDefaultValueSql("NEWID()");

                    builder.Property(b => b.BookingNumber)
                    .ValueGeneratedOnAdd();
                }
            );
        }
    }
}
