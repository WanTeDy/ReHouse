using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.FrontEnd.Models
{
    public class LoadAdvertModel
    {
        public Advert Advert { get; set; }        
        public List<Advert> OtherAdverts { get; set; }
        public Dictionary<AdvertProperty, AdvertPropertyValue> Properties { get; set; }
        public Int32 Square { get; set; }
    }
}