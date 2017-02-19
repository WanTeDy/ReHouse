using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.FrontEnd.Models
{
    public class LoadNewBuildingsModel
    {
        public List<NewBuilding> NewBuildings { get; set; }
        public List<Builder> Builders { get; set; }
        public List<PriceFilter> Prices { get; set; }
        public List<District> Districts { get; set; }
        public List<ExpluatationDate> ExpluatationDates { get; set; }
    }
}