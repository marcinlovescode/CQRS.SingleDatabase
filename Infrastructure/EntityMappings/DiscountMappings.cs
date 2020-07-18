using System;
using Domain.Discounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityMappings
{
    internal class DiscountMappings : IEntityTypeConfiguration<Discount>

    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasField("_code");
            //builder.Property<string>("_code").HasColumnName("Code");
            builder.Property<Guid?>("_subscriberId").HasColumnName("SubscriberId").IsRequired(false);
        }
    }
}