using Microsoft.EntityFrameworkCore;

namespace StoreAPI.Entities
{
    public partial class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

    }
}
