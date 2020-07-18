using Application.Orders.Projections;
using Infrastructure.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DatabaseContexts
{
    public class ReadDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public ReadDbContext(DbContextOptions<ReadDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<OrderProjection> Orders { get; set; }
        public DbSet<OrderFromNewsletterProjection> DiscountedOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderProjectionMappings());
            modelBuilder.ApplyConfiguration(new OrderFromNewsletterProjectionMappings());
        }
    }
}