using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlusDashData.Data.Models.Content;

namespace PlusDashData.Data.ViewModels.Content
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
