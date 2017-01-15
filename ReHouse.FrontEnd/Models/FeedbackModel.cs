using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class FeedbackModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(60, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        public String FirstName { get; set; }
        
        [Display(Name = "Email")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        [EmailAddress(ErrorMessage = "Не правильный email")]
        public String Email { get; set; }
        
        [Display(Name = "Сообщение")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(2000, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 25)]
        public String Message { get; set; }
    }
}