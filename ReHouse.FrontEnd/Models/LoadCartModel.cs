using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.Helpers;

namespace ReHouse.FrontEnd.Models
{
    public class LoadCartModel
    {
        public List<CartAdvertModel> Adverts { get; set; }
    }
}