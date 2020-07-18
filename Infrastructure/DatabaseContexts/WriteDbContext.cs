using Domain.Discounts;
using Domain.Newsletters;
using Domain.Orders;
using Infrastructure.EntityMappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DatabaseContexts
{
    public class WriteDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public WriteDbContext(DbContextOptions<WriteDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiscountMappings());
            modelBuilder.ApplyConfiguration(new SubscriberMappings());
            modelBuilder.ApplyConfiguration(new OrderMappings());
        }
    }
}