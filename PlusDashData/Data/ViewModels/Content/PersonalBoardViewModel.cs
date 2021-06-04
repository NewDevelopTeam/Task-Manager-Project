using System.Collections.Generic;
using PlusDashData.Data.Models.Content;

namespace PlusDashData.Data.ViewModels.Content
{
    public class PersonalBoardViewModel
    {
        public string ColumnName { get; set; }
        public List<PersonalDashboard_column> DashboardColumns { get; set; }
    }
}
