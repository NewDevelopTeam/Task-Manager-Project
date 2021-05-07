using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusDashData.Data.BoardsPageModels
{
    public class PersonalDashboard
    {
        public int Id { get; set; }
        public string DashboardName { get; set; }
        public int PositionNo { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
