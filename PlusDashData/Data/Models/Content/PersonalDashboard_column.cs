using System;

namespace PlusDashData.Data.Models.Content
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
