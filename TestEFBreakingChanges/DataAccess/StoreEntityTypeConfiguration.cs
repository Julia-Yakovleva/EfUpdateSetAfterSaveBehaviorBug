using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEFBreakingChanges.Models;

namespace TestEFBreakingChanges.DataAccess;

public class StoreEntityTypeConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder
            .ToTable("store")
            .HasKey(store => store.StoreId);

        builder
            .Property(store => store.StoreId)
            .HasColumnName("storeId");

        builder
            .Property(store => store.Name)
            .HasColumnName("name");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("createdAt")
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); // Never generate `UPDATE` for CreatedAt

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("updatedAt")
            .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);  // Never generate `INSERT` for UpdatedAt

        builder
            .HasMany<Item>(store => store.Items)
            .WithOne();

        builder.Navigation(sr => sr.Items).AutoInclude();
    }
}
