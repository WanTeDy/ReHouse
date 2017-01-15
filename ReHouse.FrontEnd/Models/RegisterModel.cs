using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class RegisterModel
    {

        //[Display(Surname = "Фамилия")]
        //[Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        //[StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 3)]
        //public String Surname { get; set; }
        

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 3)]
        public String Name { get; set; }


        //[Display(SecondName = "Отчество")]
        //[Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        //[StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 3)]
        //public String SecondName { get; set; }

        //[Display(Company = "Компания")]
        //[Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        //[StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 3)]
        //public String Company { get; set; }

        //[Display(Phone = "Телефон")]
        //[Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        //[StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 3)]
        //public String Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [StringLength(120, ErrorMessage = "* Поле {0} должно быть больше {2} и меньше {1} символов.", MinimumLength = 4)]
        [EmailAddress(ErrorMessage = "Не правильный email")]
        public String Email { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "* Поле {0} должно быть установлено.")]
        [DataType(DataType.Password)]
        [StringLength(25, ErrorMessage = "* {0} должен быть больше чем {2} и меньше чем {1}.", MinimumLength = 8)]
        //[RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Должны быть строчные и прописные латинские буквы, цифры или спецсимволы. Минимум 8 символов")]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\n])(?=.*[A-Za-z]).*$", ErrorMessage = "Пароль должен состоять из латинских букв, цифр и/или спецсимволов. Минимум 8 символов")]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}