#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.BusinessOperations.Cart;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.BusinessOperations.Home;
using ReHouse.Utils.Helpers;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.TagPages;
using ReHouse.Utils;

namespace ReHouse.FrontEnd.Controllers
{
    public class SpecialController : BaseController
    {
        public ActionResult Sale(string id)
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            //var operationFilter = new LoadFiltersOperation(tokenHash, AdvertsType.Sale, (int)ParrentCategories.Flat);
            //operationFilter.ExcecuteTransaction();
            
            var operation = new LoadFlatsByTagOperation(tokenHash, 1, ConstV.ItemsPerPage, id);
            operation.ExcecuteTransaction();
            if (operation._tagPage == null)
                return HttpNotFound();
            ViewBag.NoElements = false;
            ViewBag.Type = AdvertsType.Sale;
            ViewBag.TagPage = id;
            if (operation._adverts == null || operation._adverts.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._adverts);
        }
        [HttpPost]
        public ActionResult Load(string tagPageName, int pageNumber)
        {
            if (pageNumber < 1)
                return Json(new { noElements = true });
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadFlatsByTagOperation(tokenHash, pageNumber, ConstV.ItemsPerPage, tagPageName);
            if (operation._adverts == null || operation._adverts.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Advert/_listOfAdverts", operation._adverts);
        }
    }
}