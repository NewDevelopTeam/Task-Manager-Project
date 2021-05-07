using Microsoft.EntityFrameworkCore;

namespace External_API.Data
{
    public class AccountContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}