using System;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.FrontEnd.Models
{
    public class PageAndFilterForAdvertsModel
    {
        public Int32 DistrictId { get; set; }
        
        public Int32 Price { get; set; }
        
        public Int32 CategoryId { get; set; }

        public Int32 TrimconditionId { get; set; }

        public Int32 PageNumber { get; set; }
    }
}