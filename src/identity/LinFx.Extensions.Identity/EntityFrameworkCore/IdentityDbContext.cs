using LinFx.Extensions.Identity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinFx.Extensions.Identity.EntityFrameworkCore
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public IdentityDbContext(DbContextOptions options) : base(options) { }
    }
}
