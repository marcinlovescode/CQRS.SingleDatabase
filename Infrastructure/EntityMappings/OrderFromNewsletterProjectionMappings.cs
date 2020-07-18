using Application.Orders.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityMappings
{
    internal class OrderFromNewsletterProjectionMappings : IEntityTypeConfiguration<OrderFromNewsletterProjection>
    {
        public void Configure(EntityTypeBuilder<OrderFromNewsletterProjection> builder)
        {
            builder.ToTable("OrderFromNewsletterView");
            builder.HasNoKey();

            builder.Property(x => x.OrderId);
            builder.Property(x => x.Value).HasColumnType("decimal(14,4)");
            ;
            builder.Property(x => x.DiscountValue).HasColumnType("decimal(14,4)");
            ;
            builder.Property(x => x.TotalValue).HasColumnType("decimal(14,4)");
            ;
            builder.Property(x => x.SubscriberEmail);
        }
    }
}