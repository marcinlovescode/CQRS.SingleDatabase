using System;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityMappings
{
    internal class OrderMappings : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property<Guid?>("_discountId").HasColumnName("DiscountId").IsRequired(false);
            builder.Property<decimal>("_value").HasColumnName("Value").HasColumnType("decimal(14,4)").IsRequired();
            builder.Property<decimal>("_discountValue").HasColumnName("DiscountValue").HasColumnType("decimal(14,4)").IsRequired();
            builder.Property<decimal>("_totalValue").HasColumnName("TotalValue").HasColumnType("decimal(14,4)").IsRequired();
        }
    }
}