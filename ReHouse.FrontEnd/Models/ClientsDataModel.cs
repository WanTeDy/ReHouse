using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class ClientsDataModel
    {
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(60, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        public String Name { get; set; }
        
        [Display(Name = "Фамилия")]
        //[Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(60, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        public String SecondName { get; set; }
        
        [Display(Name = "Отчество")]
        //[Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(60, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        public String FatherName { get; set; }
        
        [Display(Name = "Email")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        [EmailAddress(ErrorMessage = "Не правильный email")]
        public String Email { get; set; }
        
        [Display(Name = "Телефон")]
        [StringLength(13, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 8)]
        [RegularExpression("^([+0-9 ']+)$", ErrorMessage = "Поле телефон должно содержать только цифры")]
        public String Phone { get; set; }
       
    }
}