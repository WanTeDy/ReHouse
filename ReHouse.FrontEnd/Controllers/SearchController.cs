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
using ReHouse.Utils.BusinessOperations.Common;
using ReHouse.Utils;

namespace ReHouse.FrontEnd.Controllers
{
    public class SearchController : BaseController
    {
        public ActionResult Flats(string query)
        {
            ViewBag.NoElements = true;
            ViewBag.Query = query;
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;

            if (!String.IsNullOrEmpty(query))
            {
                var operation = new SearchAdvertsOperation(tokenHash, query, 1, ConstV.ItemsPerPage);
                operation.ExcecuteTransaction();
                ViewBag.NoElements = false;

                if (operation._adverts == null || operation._adverts.Count < 1)
                    return RedirectToAction("Flat", "Sale");

                var model = new LoadCartModel()
                {
                    Adverts = operation._adverts,
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Load(string query, int pageNumber)
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;

            if (!String.IsNullOrEmpty(query))
            {
                var operation = new SearchAdvertsOperation(tokenHash, query, pageNumber, ConstV.ItemsPerPage);
                operation.ExcecuteTransaction();

                if (operation._adverts == null || operation._adverts.Count == 0)
                    return Json(new { noElements = true });

                var model = new LoadCartModel()
                {
                    Adverts = operation._adverts,
                };
                return PartialView("Search/_listOfAdverts", operation._adverts);
            }
            return Json(new { noElements = true });
        }
    }
}