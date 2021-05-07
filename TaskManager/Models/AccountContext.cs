using Microsoft.EntityFrameworkCore;
using PlusDashData.Data;
using PlusDashData.Data.BoardsPageModels;

namespace External_API.Data
{
    public class AccountContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PersonalCard> PerCards { get; set; }
        public DbSet<PersonalDashboard> PerDashBoards { get; set; }
        public DbSet<MultiDashboard> MultiDashBoards { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}