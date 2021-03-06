﻿using System;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.FrontEnd.Models
{
    public class PageAndFilterModel
    {
        public Int32 DistrictId { get; set; }
        
        public Int32 PriceMin { get; set; }

        public Int32 PriceMax { get; set; }
        
        public Int32 BuilderId { get; set; }

        public Int32 UserId { get; set; }

        public Int32 ExpluatationDateId { get; set; }

        public Int32 PageNumber { get; set; }

        public Boolean IsOnlyUser { get; set; }

        public Int32[] AdvertsId { get; set; }
    }
}