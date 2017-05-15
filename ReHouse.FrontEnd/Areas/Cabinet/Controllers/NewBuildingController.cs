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

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class NewBuildingController : Controller
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
            var operation = new LoadNewBuildingsOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, 0, 0, 0, 0, 0, true);
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
                pageAndFilter.DistrictId, pageAndFilter.Price, pageAndFilter.BuilderId, pageAndFilter.ExpluatationDateId, pageAndFilter.UserId, true);
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

            ViewBag.Districts = operationFilter._districts;
            ViewBag.BuildersList = operationFilter._builders;
            ViewBag.ExpluatationDates = operationFilter._expluatationDates;            

            return View(operation._newBuilding);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(NewBuilding model, HttpPostedFileBase[] images, HttpPostedFileBase[] planimages)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateNewBuildingOperation(sessionModel.TokenHash, model, images, planimages);
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

        [HttpPost]
        public ActionResult Delete(PageAndFilterModel pageAndFilter)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var op = new DeleteNewBuildingOperation(sessionModel.TokenHash, pageAndFilter.AdvertsId);
            op.ExcecuteTransaction();

            var operation = new LoadNewBuildingsOperation(sessionModel.TokenHash, pageAndFilter.PageNumber, ConstV.ItemsPerPageAdmin,
                pageAndFilter.DistrictId, pageAndFilter.Price, pageAndFilter.BuilderId, pageAndFilter.ExpluatationDateId, pageAndFilter.UserId, true);
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
        public ActionResult Add(NewBuilding model, HttpPostedFileBase[] images, HttpPostedFileBase[] planimages)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddNewBuildingOperation(sessionModel.TokenHash, model, images, planimages);
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