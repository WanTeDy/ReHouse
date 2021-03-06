﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.FrontEnd.Models
{
    public class LoadAdvertsModel
    {
        public List<Advert> Adverts { get; set; }
        public List<Category> Categories { get; set; }
        public List<PriceFilter> Prices { get; set; }
        public List<District> Districts { get; set; }
        public List<TrimCondition> TrimConditions { get; set; }
        public List<User> Users { get; set; }
        public Int32 CategoryId { get; set; }
    }
}