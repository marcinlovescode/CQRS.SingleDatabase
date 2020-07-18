using Domain.Newsletters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityMappings
{
    internal class SubscriberMappings : IEntityTypeConfiguration<Subscriber>

    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.ToTable("Subscribers");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_email").HasColumnName("Email");
        }
    }
}