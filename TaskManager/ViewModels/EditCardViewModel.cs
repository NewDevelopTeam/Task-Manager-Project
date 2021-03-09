using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels
{
    public class EditCardViewModel
    {
        public string CardId { get; set; }
        public string CardText { get; set; }
    }
}
