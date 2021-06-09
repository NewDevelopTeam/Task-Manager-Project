using System.ComponentModel.DataAnnotations;

namespace PlusDashData.Data.ViewModels.Content
{
    public class CardsViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage ="Заполните форму карточки")]
        public string CardDescription { get; set; }
    }
}
