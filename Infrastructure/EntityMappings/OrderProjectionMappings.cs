using Application.Orders.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityMappings
{
    internal class OrderProjectionMappings : IEntityTypeConfiguration<OrderProjection>
    {
        public void Configure(EntityTypeBuilder<OrderProjection> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.OrderId);
            builder.Property(x => x.OrderId).HasColumnName("Id");
            builder.Property(x => x.Value).HasColumnName("Value").HasColumnType("decimal(14,4)");
        }
    }
}