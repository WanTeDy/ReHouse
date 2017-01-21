using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class LoginModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        [EmailAddress(ErrorMessage = "Неправильный email")]
        public String Login { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [DataType(DataType.Password)]
        [StringLength(25, ErrorMessage = "* {0} должен быть больше чем {2} и меньше чем {1}.", MinimumLength = 8)]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\n])(?=.*[A-Za-z]).*$", ErrorMessage = "Пароль должен состоять из латинских букв, цифр и/или спецсимволов. Минимум 8 символов")]
        public String Password { get; set; }
    }
}