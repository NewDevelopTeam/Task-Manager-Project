using PlusDashData.Data.BoardsPageModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.ViewModels.BoardsPageViewModels
{
    public class BoardsPageViewModel
    {
        [MaxLength(20, ErrorMessage = "Название доски не более 20 символов")]
        [Required(ErrorMessage = "Укажите название персональной доски")]
        public string PersonalBoardName { get; set; }

        [MaxLength(20,ErrorMessage ="Название доски не более 20 символов")]
        [Required(ErrorMessage = "Укажите название многопользовательской доски")]
        public string MultiBoardName { get; set; }
        public List<PersonalDashboard> PersonalDashboards { get; set; }
        public List<MultiDashboard> MultiDashboards { get; set; }
    }
}
