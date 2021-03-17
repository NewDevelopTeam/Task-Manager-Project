using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class PersonalCard
    {
        public int Id { get; set; }
        public string CardDescription { get; set; }
        public int RowNo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
