using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Models
{
    public class AccountContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options):base(options)
        {
            
        }

    }
}
