using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class RecoveryPasswordModel
    {
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [DataType(DataType.Password)]
        [StringLength(25, ErrorMessage = "* {0} должен быть больше чем {2} и меньше чем {1}.", MinimumLength = 4)]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Должны быть строчные и прописные латинские буквы, цифры или спецсимволы. Минимум 8 символов")]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }

        public string TokenHash { get; set; }
    }
}