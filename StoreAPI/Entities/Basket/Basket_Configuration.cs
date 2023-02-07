using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreAPI.Entities
{
    public partial class StoreDbContext : DbContext
    {
        public virtual DbSet<Basket> Baskets { get; set; }
    }

    public class BasketEntityConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Basket");

            builder.HasKey(m => m.BasketKey);
            builder.Property(m => m.BasketKey).ValueGeneratedOnAdd();
            builder.Property(m => m.BasketId).IsRequired(true).HasMaxLength(128);
        }
    }
}
