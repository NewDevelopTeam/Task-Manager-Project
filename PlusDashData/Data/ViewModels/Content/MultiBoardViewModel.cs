using System.ComponentModel.DataAnnotations;

namespace PlusDashData.Data.ViewModels.Content
{
    public class MultiBoardViewModel
    {
        public int UserId { get; set; }

        [MaxLength(20, ErrorMessage = "The dashboard name is too long")]
        [Required(ErrorMessage = "The dashboard name can't be empty")]
        public string DashboardName { get; set; }
    }
}
