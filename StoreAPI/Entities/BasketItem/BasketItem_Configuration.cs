using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreAPI.Entities
{
    public partial class StoreDbContext : IdentityDbContext<User, Role, Guid>
    {
        public virtual DbSet<BasketItem> BasketItems { get; set; }
    }

    public class BasketItemEntityConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable("BasketItem");

            builder.HasKey(m => m.BasketItemKey);
            builder.Property(m => m.BasketItemKey).ValueGeneratedOnAdd();

            builder.HasOne(m => m.Basket)
                .WithMany(m => m.BasketItems)
                .HasForeignKey(m => m.BasketKey);

            builder.HasOne(m => m.Product)
                .WithMany()
                .HasForeignKey(m => m.ProductKey);
            
            builder.Property(m => m.Quantity).IsRequired(true);
        }
    }
}
