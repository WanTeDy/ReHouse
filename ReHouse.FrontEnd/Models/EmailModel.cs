using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class EmailModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(20, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        public String FirstName { get; set; }
        
        [Display(Name = "Email")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(30, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        [EmailAddress(ErrorMessage = "Не правильный email")]
        public String Email { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(20, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 6)]
        [Phone(ErrorMessage = "Не правильный телефон")]
        public String Phone { get; set; }

        [Display(Name = "Сообщение")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(500, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 10)]
        public String Message { get; set; }
    }
}