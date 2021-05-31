using System.ComponentModel.DataAnnotations;

namespace PlusDashData.Data.ViewModels
{
    public class CardsViewModel
    {
        [Required(ErrorMessage ="Заполните форму карточки")]
        public string CardDescription { get; set; }
    }
}
