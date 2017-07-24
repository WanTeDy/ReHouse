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
            if (String.IsNullOrEmpty(id))
                return RedirectToAction("Index", "Home");
            var operation = new LoadFlatsByTagOperation(tokenHash, 1, ConstV.ItemsPerPage, id, ParrentCategories.Flat);
            operation.ExcecuteTransaction();
            if (operation._tagPage == null)
                return HttpNotFound();
            ViewBag.NoElements = false;
            ViewBag.Type = AdvertsType.Sale;
            if (operation._adverts == null || operation._adverts.Count == 0)
                ViewBag.NoElements = true;
            ViewBag.TagPage = operation._tagPage;
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
            var operation = new LoadFlatsByTagOperation(tokenHash, pageNumber, ConstV.ItemsPerPage, tagPageName, ParrentCategories.Flat);
            operation.ExcecuteTransaction();
            if (operation._adverts == null || operation._adverts.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Advert/_listOfAdverts", operation._adverts);
        }
    }
}


//var db = new DbReHouse();
//var districts = db.Districts.ToList();
//foreach (var dist in districts)
//{
//    db.TagPages.Add(new Utils.DataBase.AdvertParams.TagPage
//    {
//        AdvertsType = AdvertsType.Sale,
//        RussianName = "Продажа квартир в " + dist.RussianName,
//        SeoText = "Текст",
//        ShortName =  TranslitHelper.Front("Продажа квартир в " + dist.RussianName),
//        TagPageType = TagPageType.District,
//        District = dist,
//    });
//}

//var cats = db.Categories.Where(x=>x.ParentId == 1).ToList();
//foreach (var cat in cats)
//{
//    db.TagPages.Add(new Utils.DataBase.AdvertParams.TagPage
//    {
//        AdvertsType = AdvertsType.Sale,
//        RussianName = "Продажа " + cat.RussianName + " квартир",
//        SeoText = "Текст",
//        ShortName = TranslitHelper.Front("Продажа " + cat.RussianName + " квартир"),
//        TagPageType = TagPageType.Category,
//        Category = cat,
//    });
//}
//db.SaveChanges();