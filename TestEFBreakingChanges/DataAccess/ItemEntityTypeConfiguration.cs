using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestEFBreakingChanges.Models;

namespace TestEFBreakingChanges.DataAccess;

public class ItemEntityTypeConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .ToTable("item")
            .HasKey("StoreId", "ItemCode");

        builder
            .Property(item => item.ItemCode)
            .HasColumnName("itemCode");

        builder
            .Property(item => item.Name)
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
            .HasOne<Store>()
            .WithMany(store => store.Items);
    }
}
