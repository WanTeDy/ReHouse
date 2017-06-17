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
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;

            if (!String.IsNullOrEmpty(query))
            {
                var operation = new SearchAdvertsOperation(tokenHash, query, 1, ConstV.ItemsPerPage);
                operation.ExcecuteTransaction();

                if (operation._adverts != null && operation._adverts.Count > 0)
                    ViewBag.NoElements = false;
                var model = new LoadCartModel()
                {
                    Adverts = operation._adverts,
                };
                return View(model);
            }
            return View();
        }

        //[HttpPost]
        //public JsonResult Load(CartModel model)
        //{
        //    var cart = SessionHelpers.Session("Cart") as List<CartModel> ?? new List<CartModel>();
        //    var adv = cart.Find(x => x.AdvertId == model.AdvertId && x.Type == model.Type);
        //    if (adv == null && model.IsAdd)
        //    {
        //        cart.Add(model);
        //    }
        //    else if (adv != null && !model.IsAdd)
        //    {
        //        cart.Remove(adv);
        //    }
        //    SessionHelpers.Session("Cart", cart);
        //    return Json(new { NoError = true });
        //}
    }
}