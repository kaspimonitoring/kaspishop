using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProductOriginalConfiguration : IEntityTypeConfiguration<ProductOriginal>
{
    public void Configure(EntityTypeBuilder<ProductOriginal> builder)
    {
        builder
             .HasKey(product => product.Id);

        builder
            .HasIndex(product => product.ExternalId)
            .IsUnique();
        builder
            .Property(x => x.ExternalId)
            .HasMaxLength(9)
            .IsRequired();

        builder
            .Property(x => x.Json)
            .HasColumnType(Constants.JSONB)
            .IsRequired();

        builder
            .Property(x => x.Hash)
            .HasMaxLength(64)
            .IsRequired();
    }
}