using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class RestorePasswordModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        [EmailAddress(ErrorMessage = "Не правильный email")]
        public String Email { get; set; }

        public String TokenHash { get; set; }
    }
}