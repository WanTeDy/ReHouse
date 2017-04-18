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
using System.Web;

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
        
        [ValidateAntiForgeryToken]        
        [HttpPost]
        public ActionResult Edit(Advert model, HttpPostedFileBase[] images, HttpPostedFileBase[] planimages)
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