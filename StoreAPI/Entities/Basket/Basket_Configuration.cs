using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreAPI.Entities
{
    public partial class StoreDbContext : IdentityDbContext<User, Role, Guid>
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
            builder.Property(m => m.UserKey).IsRequired(true);
        }
    }
}
