using Microsoft.EntityFrameworkCore;
using PlusDashData.Data.Models.Content;

namespace Dashboards_API.Models
{
    public class DashboardsContext : DbContext
    {
        public DbSet<PersonalCard> PerCards { get; set; }
        public DbSet<PersonalDashboard> PerDashBoards { get; set; }
        public DbSet<MultiDashboard> MultiDashBoards { get; set; }
        public DashboardsContext(DbContextOptions<DashboardsContext> options) : base(options)
        {
            
        }
    }
}
