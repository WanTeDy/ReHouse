using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        private Int32 _articlesCount = 2;

        public BaseController()
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;

            var operation = new LoadArticlesOperation(tokenHash, 1, _articlesCount);
            operation.ExcecuteTransaction();
            ViewBag.Articles = operation._articles;
        }
    }
}