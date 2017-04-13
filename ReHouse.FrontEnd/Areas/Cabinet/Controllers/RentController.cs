#define CASH

using System;
using System.Net;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.BusinessOperations.Filters;
using ReHouse.Utils.BusinessOperations.Flat;
using ReHouse.Utils;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using Newtonsoft.Json;
using ReHouse.Utils.BusinessOperations.Titles;
using ReHouse.Utils.BusinessOperations.Categories;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class RentController : Controller
    {
        [HttpGet]
        public ActionResult Flat(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            int categoryId = id ?? (int)ParrentCategories.Flat;
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.Rent, (int)ParrentCategories.Flat);
            operationFilter.ExcecuteTransaction();
            var model = new LoadAdvertsModel
            {
                Districts = operationFilter._districts,
                Categories = operationFilter._categories,
                Prices = operationFilter._prices,
                TrimConditions = operationFilter._trimConditions,
                CategoryId = categoryId,
            };
            var operation = new LoadFlatsOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, 0, 0, 0, categoryId, AdvertsType.Rent, false);
            operation.ExcecuteTransaction();
            if (operation._category == null)
                return HttpNotFound();
            model.Adverts = operation._adverts;
            ViewBag.NoElements = false;
            if (operation._adverts == null || operation._adverts.Count == 0)
                ViewBag.NoElements = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult House(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            int categoryId = id ?? (int)ParrentCategories.House;
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.Rent, (int)ParrentCategories.House);
            operationFilter.ExcecuteTransaction();
            var model = new LoadAdvertsModel
            {
                Districts = operationFilter._districts,
                Categories = operationFilter._categories,
                Prices = operationFilter._prices,
                TrimConditions = operationFilter._trimConditions,
                CategoryId = categoryId,
            };
            var operation = new LoadFlatsOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, 0, 0, 0, categoryId, AdvertsType.Rent, false);
            operation.ExcecuteTransaction();
            if (operation._category == null)
                return HttpNotFound();
            model.Adverts = operation._adverts;
            ViewBag.NoElements = false;
            if (operation._adverts == null || operation._adverts.Count == 0)
                ViewBag.NoElements = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Homestead(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            int categoryId = id ?? (int)ParrentCategories.Homestead;
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.Rent, (int)ParrentCategories.Homestead);
            operationFilter.ExcecuteTransaction();
            var model = new LoadAdvertsModel
            {
                Districts = operationFilter._districts,
                Categories = operationFilter._categories,
                Prices = operationFilter._prices,
                //TrimConditions = operationFilter._trimConditions,
                CategoryId = categoryId,
            };
            var operation = new LoadFlatsOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, 0, 0, 0, categoryId, AdvertsType.Rent, false);
            operation.ExcecuteTransaction();
            if (operation._category == null)
                return HttpNotFound();
            ViewBag.NoElements = false;
            model.Adverts = operation._adverts;
            if (operation._adverts == null || operation._adverts.Count == 0)
                ViewBag.NoElements = true;
            return View(model);
        }

        [HttpGet]
        public ActionResult Commerce(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            int categoryId = id ?? (int)ParrentCategories.Commerce;
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.Rent, (int)ParrentCategories.Commerce);
            operationFilter.ExcecuteTransaction();
            var model = new LoadAdvertsModel
            {
                Districts = operationFilter._districts,
                Categories = operationFilter._categories,
                Prices = operationFilter._prices,
                TrimConditions = operationFilter._trimConditions,
                CategoryId = categoryId,
            };
            var operation = new LoadFlatsOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, 0, 0, 0, categoryId, AdvertsType.Rent, false);
            operation.ExcecuteTransaction();
            if (operation._category == null)
                return HttpNotFound();
            ViewBag.NoElements = false;
            model.Adverts = operation._adverts;
            if (operation._adverts == null || operation._adverts.Count == 0)
                ViewBag.NoElements = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Load(PageAndFilterForAdvertsModel pageAndFilter)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            if (pageAndFilter.PageNumber < 1)
                return Json(new { noElements = true });
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var operation = new LoadFlatsOperation(sessionModel.TokenHash, pageAndFilter.PageNumber, ConstV.ItemsPerPageAdmin, pageAndFilter.DistrictId, pageAndFilter.Price,
                pageAndFilter.TrimconditionId, pageAndFilter.CategoryId, AdvertsType.Rent, false, pageAndFilter.IsOnlyUser);
            operation.ExcecuteTransaction();
            if (operation._category == null || operation._adverts == null || operation._adverts.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Advert/_listOfAdverts", operation._adverts);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            int Id = id ?? 0;
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            if (Id == 0)
                return HttpNotFound();
            var operation = new LoadFlatOperation(sessionModel.TokenHash, Id, 0, 0);
            operation.ExcecuteTransaction();
            if (operation._advert == null)
                return HttpNotFound();

            var op = new LoadTitlesOperation(sessionModel.TokenHash);
            op.ExcecuteTransaction();
            ViewBag.Titles = op._titles;

            var op2 = new LoadMarketTypesOperation(sessionModel.TokenHash);
            op2.ExcecuteTransaction();
            ViewBag.MarketTypes = op2._marketTypes;

            var op3 = new LoadTrimConditionsOperation(sessionModel.TokenHash);
            op3.ExcecuteTransaction();
            ViewBag.TrimConditions = op3._trimConditions;

            var op4 = new LoadCategoriesOperation(sessionModel.TokenHash, operation._advert.Category.ParentId.Value);
            op4.ExcecuteTransaction();
            ViewBag.Categories = op4._categories;
            
            return View(operation._advert);
        }

        [HttpPost]
        public ActionResult Edit(Advert model)
        {

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Add(NewBuildingModel newBuilding)
        {

            return View(newBuilding);
        }
    }
}
//DbReHouse db = new DbReHouse();
//db.TrimConditions.Add(new TrimCondition { RussianName = "Под чистовую" });
//            db.TrimConditions.Add(new TrimCondition { RussianName = "Удовлетворительное" });
//            db.TrimConditions.Add(new TrimCondition { RussianName = "Хорошее" });
//            db.TrimConditions.Add(new TrimCondition { RussianName = "Евроремонт" });
//            db.TrimConditions.Add(new TrimCondition { RussianName = "Люкс" });
//            db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Квартиры" });
//            db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Дома" });
//            db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Участки" });
//            db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Коммерческая" });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "1-комнатные", ParentId = 1 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "2-комнатные", ParentId = 1 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "3-комнатные", ParentId = 1 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "4-комнатные", ParentId = 1 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "5-комнатные", ParentId = 1 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Дома", ParentId = 2 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Часть дома", ParentId = 2 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Таунхаусы", ParentId = 2 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Дачи", ParentId = 2 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Участки", ParentId = 3 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Офисы", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Помещения", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Магазины", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Склады", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Здания", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Автомойки", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Базы отдыха", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Бары", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Гостиницы", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Готовый бизнес", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Парикмахерские", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Производства", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Рестораны", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Салоны красоты", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "СТО", ParentId = 4 });
//db.Categories.Add(new Utils.DataBase.AdvertParams.Category { RussianName = "Фитнес клубы", ParentId = 4 });

//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "1-комнатную квартиру",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "2-комнатную квартиру",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "3-комнатную квартиру",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "4-комнатную квартиру",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "5-комнатныую квартиру",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Дом",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Часть дома",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Таунхаус", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Дачу",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Участок",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Офис",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Помещение", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Магазин", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Склад", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Здание", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Автомойку", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Базу отдыха",});
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Бар", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Гостиницу", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Готовый бизнес",  });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Парикмахерскую",  });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Производство", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Ресторан",  });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Салон красоты",  });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "СТО", });
//db.Titles.Add(new Utils.DataBase.AdvertParams.Title { RussianName = "Фитнес клуб", });


//            db.SaveChanges();

//DbReHouse db = new DbReHouse();
//for (int i = 0; i < 150; i++)
//{
//    var el = new Advert
//    {
//        CategoryId = new Random().Next(5, 31),
//        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit",
//        DistrictId = new Random().Next(1, 22),
//        ExpireDate = DateTime.Now.AddDays(30),
//        IsHot = new Random().Next(0, 2) > 0 ? true : false,
//        PublicationDate = DateTime.Now,
//        Price = new Random().Next(5000, 500000),
//        Street = "Улица Конная 1",
//        TitleId = new Random().Next(1, 27),
//        UserId = 2,
//        YouTubeUrl = @"https://www.youtube.com/embed/pP9YV_P6WsM",
//    };
//    if(i%2 == 0)
//    {
//        el.TrimConditionId = new Random().Next(1, 6);
//    }
//    db.Adverts.Add(el);
//    Thread.Sleep(1000);
//}
//db.SaveChanges();

//DbReHouse db = new DbReHouse();
//var prop = new AdvertProperty
//{
//    RussianName = "Количество помещений",
//    Priority = 40,
//};
//db.AdvertProperties.Add(prop);
//var cat1 = db.Categories.FirstOrDefault(x => x.Id == 1);

//var cat2 = db.Categories.FirstOrDefault(x => x.Id == 2);

//var cat3 = db.Categories.FirstOrDefault(x => x.Id == 3);

//var cat4 = db.Categories.FirstOrDefault(x => x.Id == 4);
//cat4.AdvertProperties.Add(prop);

//prop = new AdvertProperty
//{
//    RussianName = "Общая площадь, м²",
//    Priority = 1,
//};
//db.AdvertProperties.Add(prop);
//cat1.AdvertProperties.Add(prop);
//cat2.AdvertProperties.Add(prop);

//prop = new AdvertProperty
//{
//    RussianName = "Этаж",
//    Priority = 80,
//};
//db.AdvertProperties.Add(prop);
//cat1.AdvertProperties.Add(prop);            
//cat4.AdvertProperties.Add(prop);

//prop = new AdvertProperty
//{
//    RussianName = "Этажность",
//    Priority = 90,
//};
//db.AdvertProperties.Add(prop);
//cat1.AdvertProperties.Add(prop);
//cat2.AdvertProperties.Add(prop);
//cat4.AdvertProperties.Add(prop);

//prop = new AdvertProperty
//{
//    RussianName = "Площадь участка, сотки",
//    Priority = 50,
//};
//db.AdvertProperties.Add(prop);
//cat2.AdvertProperties.Add(prop);
//cat3.AdvertProperties.Add(prop);

//prop = new AdvertProperty
//{
//    RussianName = "Жилая площадь, м²",
//    Priority = 20,
//};
//db.AdvertProperties.Add(prop);
//cat1.AdvertProperties.Add(prop);
//cat2.AdvertProperties.Add(prop);

//prop = new AdvertProperty
//{
//    RussianName = "Площадь кухни, м²",
//    Priority = 30,
//};
//db.AdvertProperties.Add(prop);
//cat1.AdvertProperties.Add(prop);
//cat2.AdvertProperties.Add(prop);

//prop = new AdvertProperty
//{
//    RussianName = "Материал стен",
//    Priority = 100,
//};
//db.AdvertProperties.Add(prop);
//cat1.AdvertProperties.Add(prop);
//cat2.AdvertProperties.Add(prop);


//db.SaveChanges();


//DbReHouse db = new DbReHouse();
//var adverts = db.Adverts.ToList();
//foreach(var advert in adverts)
//{
//    Random rnd = new Random();
//    var catProp = advert.Category.Parent.AdvertProperties.ToList();
//    foreach(var prop in catProp)
//    {
//        string rndStr = rnd.Next(1, 120).ToString();
//        var val = new AdvertPropertyValue
//        {
//            AdvertId = advert.Id,
//            PropertiesValue = rndStr,
//            AdvertPropertyId = prop.Id,
//        };
//        db.AdvertPropertyValues.Add(val);
//    }                
//}
//db.SaveChanges();



