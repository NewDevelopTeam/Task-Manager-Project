using Microsoft.EntityFrameworkCore;
using PlusDashData.Data.Models.Accounts;

namespace Accounts_API
{
    public class AccountsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}