using System;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class BuilderModel
    {
        [Display(Name = "Застройщик")]
        public String BuilderName { get; set; }

        [Display(Name = "Сайт застройщика")]
        public String BuilderUrl { get; set; }
    }
}