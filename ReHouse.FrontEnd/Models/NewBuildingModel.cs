using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.FrontEnd.Models
{
    public class NewBuildingModel
    {
        [Display(Name = "Название")]
        public String Name { get; set; }

        [Display(Name ="Адресс")]     
        public String Adress { get; set; }

        [Display(Name ="Цена")]
        public Int32 Price { get; set; }

        [Display(Name = "Кол-во домов")]
        public Int32 HouseQuantity { get; set; }

        [Display(Name = "Кол-во секций")]
        public Int32 SectionQuantity { get; set; }

        [Display(Name = "Кол-во комнат")]
        public Int32 FloatQuantity { get; set; }

        [Display(Name = "Этажность")]
        public Int32 FloorQuantity { get; set; }

        [Display(Name = "Дата ввода в эксплуатацию")]
        public String ExpluatationDate { get; set; }

        [Display(Name = "Тип констукций")]
        public String Construct { get; set; }

        [Display(Name = "Материал стен")]
        public String WallMaterial { get; set; }

        [Display(Name = "Высота потолка")]
        public Double WallHeight { get; set; }

        [Display(Name = "Отопление")]
        public String Heating { get; set; }

        [Display(Name = "Парковка")]
        public String Parking { get; set; }

        [Display(Name = "Ссылка на YouTube видео")]
        public String YouTubeUrl { get; set; }

        [Display(Name = "Сайт")]
        public String Url { get; set; }

        [Display(Name = "Застройщик")]
        public String BuilderName { get; set; }

        [Display(Name = "Сайт застройщика")]
        public String BuilderUrl { get; set; }
        public List<BuilderModel> Builders { get; set; }
        public List<PhoneModel> Phones { get; set; }
    }
}