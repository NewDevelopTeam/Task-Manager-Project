using Microsoft.EntityFrameworkCore;
using PlusDashData.Data.Models.Accounts;

namespace TaskManager.Models
{
    public class AccountContext : DbContext
    {
        public DbSet<UserSession> UserSessions { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
