#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.BusinessOperations.Home;
using ReHouse.Utils.Helpers;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.News;
using ReHouse.Utils;

namespace ReHouse.FrontEnd.Controllers
{
    public class NewsController : Controller
    {        
        public ActionResult Index()
        {            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadArticlesOperation(tokenHash, 1, ConstV.ItemsPerPage);
            operation.ExcecuteTransaction();            
            return View(operation._articles);
        }

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            int Id = id ?? 0;
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            if (Id == 0)
                return HttpNotFound();
            var operation = new LoadArticleOperation(tokenHash, Id);
            operation.ExcecuteTransaction();
            if (operation._article == null)
                return HttpNotFound();
            
            return View(operation._article);
        }

        [HttpPost]
        public ActionResult Load(PageModel page)
        {
            if (page.PageNumber < 1)
                return Json(new { noElements = true });
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadArticlesOperation(tokenHash, page.PageNumber, ConstV.ItemsPerPage);
            operation.ExcecuteTransaction();
            if (operation._articles == null || operation._articles.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Article/_listOfArticles", operation._articles);
        }
    }
}