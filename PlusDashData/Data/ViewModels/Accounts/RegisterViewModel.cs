﻿using System.ComponentModel.DataAnnotations;

namespace PlusDashData.Data.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Не указана почта")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Некорректно указана почта")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [RegularExpression(@"^[\w]*$", ErrorMessage = "Допускаются только латинские буквы и цифры")]
        [StringLength(50, ErrorMessage = "Пароль должен содержать не менее {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторно укажите пароль")]
        [RegularExpression(@"^[\w]*$", ErrorMessage = "Допускаются только латинские буквы и цифры")]
        [StringLength(18, ErrorMessage = "Пароль должен содержать не менее {2} символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Введённые пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
