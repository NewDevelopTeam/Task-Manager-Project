using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models.BoardsPageModels;

namespace TaskManager.ViewModels.BoardsPageViewModels
{
    public class PersonalBoardViewModel
    {
        public string ColumnName { get; set; }
        public List<PersonalDashboard_column> DashboardColumns { get; set; }
    }
}
