using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels
{
    public class CardsViewModel
    {
        [Required(ErrorMessage ="Заполните форму карточки")]
        public string CardDescription { get; set; }
    }
}
