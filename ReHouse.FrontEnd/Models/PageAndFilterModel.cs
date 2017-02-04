using System;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.FrontEnd.Models
{
    public class PageAndFilterModel
    {
        public Int32 PageNumber { get; set; }

        public Int32 DistrictId { get; set; }
        
        public Int32 Price { get; set; }
        
        public Int32 BuilderId { get; set; }

        public Int32 ExpluatationDateId { get; set; }
    }
}