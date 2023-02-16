using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreAPI.Entities
{
    public partial class StoreDbContext : IdentityDbContext<User, Role, Guid>
    {
    }
}
