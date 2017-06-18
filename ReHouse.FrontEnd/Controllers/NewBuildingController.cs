#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.BusinessOperations.Filters;
using ReHouse.Utils.BusinessOperations.Building;
using ReHouse.Utils;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;


namespace ReHouse.FrontEnd.Controllers
{
    public class NewBuildingController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operationFilter = new LoadFiltersOperation(tokenHash, AdvertsType.NewBuilding);
            operationFilter.ExcecuteTransaction();
            var model = new LoadNewBuildingsModel
            {
                Districts = operationFilter._districts,
                Builders = operationFilter._builders,
                Prices = operationFilter._prices,
                ExpluatationDates = operationFilter._expluatationDates,
            };
            var operation = new LoadNewBuildingsOperation(tokenHash, 1, ConstV.ItemsPerPage, 0, 0, 0, 0, 0, 0);
            operation.ExcecuteTransaction();
            model.NewBuildings = operation._newBuildings;
            ViewBag.NoElements = false;
            if (operation._newBuildings == null || operation._newBuildings.Count == 0)
                ViewBag.NoElements = true;

            this.LoadPageText();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(PageAndFilterModel pageAndFilter)
        {
            if (pageAndFilter.PageNumber < 1)
                return Json(new { noElements = true });
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadNewBuildingsOperation(tokenHash, pageAndFilter.PageNumber, ConstV.ItemsPerPage, 
                pageAndFilter.DistrictId, pageAndFilter.PriceMin, pageAndFilter.PriceMax, pageAndFilter.BuilderId, pageAndFilter.ExpluatationDateId, pageAndFilter.UserId, pageAndFilter.IsOnlyUser);
            operation.ExcecuteTransaction();
            if (operation._newBuildings == null || operation._newBuildings.Count == 0)
                return Json(new { noElements = true });
            return PartialView("NewB/_listOfNewBuildings", operation._newBuildings);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadNewBuildingOperation(tokenHash, id, 1, ConstV.ItemsPerPage);
            operation.ExcecuteTransaction();
            if (operation._newBuilding == null)
                return HttpNotFound();
            return View(new LoadNewBuildingModel
            {
                NewBuilding = operation._newBuilding,
                OtherNewBuildings = operation._otherNewBuilding
            });
        }        
    }
}

//DbReHouse db = new DbReHouse();
//for (int i = 1; i < 50; i++)
//{
//    int builderId1 = new Random().Next(1, 10);
//    int builderId2 = new Random().Next(1, 10);
//    var obj = new NewBuilding
//    {
//        Adress = "Адресс новостроя: Ленина " + i,
//        Name = "ЖК Новострой " + i,
//        Price = new Random().Next(5000, 40000),
//        ExpluatationDateId = new Random().Next(1, 32),
//        PublicationDate = DateTime.Now,
//        UserId = 2,
//        DistrictId = new Random().Next(1, 21),
//};
//    var bui1 = db.Builders.Include("NewBuildings").FirstOrDefault(x => x.Id == builderId1);
//    var bui2 = db.Builders.Include("NewBuildings").FirstOrDefault(x => x.Id == builderId2);
//    bui1.NewBuildings.Add(obj);
//    bui2.NewBuildings.Add(obj);
//    Thread.Sleep(1000);
//}
//db.SaveChanges();


//DbReHouse db = new DbReHouse();
//for (int i = 1; i < 20; i++)
//{
//    var obj = new NewBuilding
//    {
//        Adress = "Адресс новостроя: аааааааа бавыдлаыа 21",
//        Name = "ЖК Новострой " + i,
//        Price = 13700,
//        ExpluatationDate = i + "-й квартал 2017 года",
//        PublicationDate = DateTime.Now,
//        UserId = 2,
//    };
//    db.NewBuildings.Add(obj);
//    Thread.Sleep(1000);
//}
//db.SaveChanges();
//DbReHouse db = new DbReHouse();
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Таирова" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Радужный" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Киевский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Черемушки" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Молдаванка" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Ленпоселок" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Малиновский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Большой Фонтан" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Центр" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Приморский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Котовского" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Лузановка" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Пересыпь" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Слободка" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Суворовский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "пригород Одессы" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Белгород-Днестровский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Беляевский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Коминтерновский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Овидиопольский" });
//db.Districts.Add(new Utils.DataBase.Geo.District { RussianName = "Одесская область" });

//for(int i = 2017; i < 2025; i++)
//{
//    db.ExpluatationDates.Add(new ExpluatationDate {Name = "1-й квартал " + i });
//    db.ExpluatationDates.Add(new ExpluatationDate {Name = "2-й квартал " + i });
//    db.ExpluatationDates.Add(new ExpluatationDate {Name = "3-й квартал " + i });
//    db.ExpluatationDates.Add(new ExpluatationDate {Name = "4-й квартал " + i });

//}
//db.SaveChanges();
//DbReHouse db = new DbReHouse();
//db.PriceFilters.Add(new PriceFilter { Min = 0, Max = 10000, AdvertType = AdvertsType.NewBuilding });
//            db.PriceFilters.Add(new PriceFilter { Min = 10000, Max = 15000, AdvertType = AdvertsType.NewBuilding });
//            db.PriceFilters.Add(new PriceFilter { Min = 15000, Max = 20000, AdvertType = AdvertsType.NewBuilding });
//            db.PriceFilters.Add(new PriceFilter { Min = 20000, Max = 25000, AdvertType = AdvertsType.NewBuilding });
//            db.PriceFilters.Add(new PriceFilter { Min = 25000, Max = 30000, AdvertType = AdvertsType.NewBuilding });
//            db.PriceFilters.Add(new PriceFilter { Min = 30000, Max = 35000, AdvertType = AdvertsType.NewBuilding });
//            db.PriceFilters.Add(new PriceFilter { Min = 35000, Max = 40000, AdvertType = AdvertsType.NewBuilding });
//            db.PriceFilters.Add(new PriceFilter { Min = 40000, Max = 45000, AdvertType = AdvertsType.NewBuilding });
//            db.SaveChanges();

//DbReHouse db = new DbReHouse();
//            for(int i = 0; i<10; i++)
//            {
//                db.Builders.Add(new Builder { Name = "Застройщик " + i, Url = "wwww.builder.com" });
//            }

//            db.SaveChanges();