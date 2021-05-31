using System.Collections.Generic;
using PlusDashData.Data.BoardsPageModels;

namespace PlusDashData.Data.ViewModels.BoardsPageViewModels
{
    public class PersonalBoardViewModel
    {
        public string ColumnName { get; set; }
        public List<PersonalDashboard_column> DashboardColumns { get; set; }
    }
}
