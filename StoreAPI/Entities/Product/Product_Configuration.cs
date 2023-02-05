using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreAPI.Entities
{
    public partial class StoreDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
    }

    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(m => m.ProductKey);
            builder.Property(m => m.ProductKey).ValueGeneratedOnAdd();

            builder.Property(m => m.ProductId).IsRequired(true).HasMaxLength(128);
            builder.Property(m => m.Name).IsRequired(true).HasMaxLength(128);
            builder.Property(m => m.Price).IsRequired(true);
            builder.Property(m => m.PictureUrl).IsRequired(false);
            builder.Property(m => m.Type).IsRequired(true);
            builder.Property(m => m.Brand).IsRequired(true);
            builder.Property(m => m.QuantityInStock).IsRequired(true);
        }
    }
}
