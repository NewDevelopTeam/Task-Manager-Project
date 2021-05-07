using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlusDashData.Data.BoardsPageModels
{
    public class PersonalDashboard_column
    {
        public int Id { get; set; }
        public string DashboardColumnName { get; set; }
        public int PositionNo { get; set; }
        public int PersonalDashboardId { get; set; }
        public PersonalDashboard PersDashBoard { get; set; }
    }
}
