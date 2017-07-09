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
using ReHouse.Utils.BusinessOperations.Filters;
using ReHouse.Utils;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using Newtonsoft.Json;
using System.Web;
using ReHouse.Utils.BusinessOperations.Building;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.BusinessOperations.Seo;
using ReHouse.Utils.DataBase.Common;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class NewBuildingController : BaseCabinetController
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.NewBuilding);
            operationFilter.ExcecuteTransaction();
            var model = new LoadNewBuildingsModel
            {
                Districts = operationFilter._districts,
                Builders = operationFilter._builders,
                Prices = operationFilter._prices,
                ExpluatationDates = operationFilter._expluatationDates,
                Users = operationFilter._users
            };
            var operation = new LoadNewBuildingsOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, 0, 0, 0, 0, 0, 0, true);
            operation.ExcecuteTransaction();
            model.NewBuildings = operation._newBuildings;
            ViewBag.NoElements = false;
            if (operation._newBuildings == null || operation._newBuildings.Count == 0)
                ViewBag.NoElements = true;
            return View(model);
        }


        [HttpPost]
        public ActionResult Load(PageAndFilterModel pageAndFilter)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            if (pageAndFilter.PageNumber < 1)
                return Json(new { noElements = true });
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var operation = new LoadNewBuildingsOperation(sessionModel.TokenHash, pageAndFilter.PageNumber, ConstV.ItemsPerPageAdmin, 
                pageAndFilter.DistrictId, pageAndFilter.PriceMin, pageAndFilter.PriceMax, pageAndFilter.BuilderId, pageAndFilter.ExpluatationDateId, pageAndFilter.UserId, true);
            operation.ExcecuteTransaction();
            if (operation._newBuildings == null || operation._newBuildings.Count == 0)
                return Json(new { noElements = true });
            return PartialView("NewB/_listOfNewBuildings", operation._newBuildings);
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
            var operation = new LoadNewBuildingOperation(sessionModel.TokenHash, Id, 0, 0, true);
            operation.ExcecuteTransaction();
            if (operation._newBuilding == null)
                return HttpNotFound();

            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.NewBuilding);
            operationFilter.ExcecuteTransaction();

            var op6 = new LoadSeoParamOperation(sessionModel.TokenHash, ConstV.DetailAction, CurrentController, "/" + CurrentController + "/" + ConstV.DetailAction + "/" + operation._newBuilding.Id, operation._newBuilding.Id.ToString());
            op6.ExcecuteTransaction();
            ViewBag.SeoParam = op6._seoParams ?? new SeoParam();

            ViewBag.Districts = operationFilter._districts;
            ViewBag.BuildersList = operationFilter._builders;
            ViewBag.ExpluatationDates = operationFilter._expluatationDates;            

            return View(operation._newBuilding);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(NewBuilding model, String[] image, String[] planimage,
            string seo_title, string seo_description, string seo_keywords, int seo_id, Image[] imageData, PlanImage[] planimageData)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateNewBuildingOperation(sessionModel.TokenHash, model, image, planimage, imageData, planimageData);
            operation.ExcecuteTransaction();

            var seoparam = new SeoParam
            {
                Id = seo_id,
                ActionName = ConstV.DetailAction,
                ControllerName = CurrentController,
                Description = seo_description,
                Keywords = seo_keywords,
                Title = seo_title,
                UrlParams = operation._newBuilding.Id.ToString(),
                FullUrl = "/" + CurrentController + "/" + ConstV.DetailAction + "/" + operation._newBuilding.Id,
            };
            ViewBag.SeoParam = seoparam;

            var operation2 = new UpdateSeoParamOperation(sessionModel.TokenHash, seoparam);
            operation2.ExcecuteTransaction();

            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.NewBuilding);
            operationFilter.ExcecuteTransaction();

            ViewBag.Districts = operationFilter._districts;
            ViewBag.BuildersList = operationFilter._builders;
            ViewBag.ExpluatationDates = operationFilter._expluatationDates;

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(PageAndFilterModel pageAndFilter)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var op = new DeleteNewBuildingOperation(sessionModel.TokenHash, pageAndFilter.AdvertsId);
            op.ExcecuteTransaction();

            var operation = new LoadNewBuildingsOperation(sessionModel.TokenHash, pageAndFilter.PageNumber, ConstV.ItemsPerPageAdmin,
                pageAndFilter.DistrictId, pageAndFilter.PriceMin, pageAndFilter.PriceMax, pageAndFilter.BuilderId, pageAndFilter.ExpluatationDateId, pageAndFilter.UserId, true);
            operation.ExcecuteTransaction();
            if (operation._newBuildings == null || operation._newBuildings.Count == 0)
                return Json(new { noElements = true });
            return PartialView("NewB/_listOfNewBuildings", operation._newBuildings);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddNewBuildingOperation(sessionModel.TokenHash, null, null, null);
            operation.ExcecuteTransaction();

            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.NewBuilding);
            operationFilter.ExcecuteTransaction();

            ViewBag.Districts = operationFilter._districts;
            ViewBag.BuildersList = operationFilter._builders;
            ViewBag.ExpluatationDates = operationFilter._expluatationDates;

            return View(new NewBuilding());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(NewBuilding model, String[] image, String[] planimage)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddNewBuildingOperation(sessionModel.TokenHash, model, image, planimage);
            operation.ExcecuteTransaction();

            var operationFilter = new LoadFiltersOperation(sessionModel.TokenHash, AdvertsType.NewBuilding);
            operationFilter.ExcecuteTransaction();

            ViewBag.Districts = operationFilter._districts;
            ViewBag.BuildersList = operationFilter._builders;
            ViewBag.ExpluatationDates = operationFilter._expluatationDates;

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }

            return RedirectToAction("List");
        }
    }
}