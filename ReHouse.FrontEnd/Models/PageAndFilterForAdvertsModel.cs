using System;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Helpers;

namespace ReHouse.FrontEnd.Models
{
    public class PageAndFilterForAdvertsModel
    {
        public Int32 DistrictId { get; set; }
        
        public Int32 PriceMin { get; set; }

        public Int32 PriceMax { get; set; }
        
        public Int32 CategoryId { get; set; }

        public Int32 TrimconditionId { get; set; }

        public Int32 UserId { get; set; }

        public Int32 PageNumber { get; set; }

        public Boolean IsOnlyUser { get; set; }

        public Int32[] AdvertsId { get; set; }

        public RentPeriodType RentPeriodType { get; set; }
    }
}