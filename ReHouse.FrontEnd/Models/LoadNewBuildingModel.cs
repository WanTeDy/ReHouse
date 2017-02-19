using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.FrontEnd.Models
{
    public class LoadNewBuildingModel
    {
        public NewBuilding NewBuilding { get; set; }
        
        public List<NewBuilding> OtherNewBuildings { get; set; }
    }
}