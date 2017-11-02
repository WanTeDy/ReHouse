using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.FrontEnd.Models
{
    public class LoadAdvertsForHomePageModel
    {
        public List<Advert> HotAdverts { get; set; }
        public List<Advert> FlatSaleAdverts { get; set; }
        public List<Advert> HouseSaleAdverts { get; set; }
        public List<Advert> CommerceSaleAdverts { get; set; }
        public List<NewBuilding> NewBuildingAdverts { get; set; }
        //public List<Article> Articles { get; set; }
    }
}