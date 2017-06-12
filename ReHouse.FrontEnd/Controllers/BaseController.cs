using System.Web.Routing;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReHouse.Utils.BusinessOperations.Seo;

namespace ReHouse.FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        private const Int32 _articlesCount = 2;

        public string CurrentAction { get; set; }
        public string CurrentController { get; set; }
        public string AbsoluteUrl { get; set; }
        public string UrlParams { get; set; }
        
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            if (HttpContext.Request.HttpMethod.ToLower() == "get")
            {
                var operation = new LoadArticlesOperation(tokenHash, 1, _articlesCount);
                operation.ExcecuteTransaction();
                ViewBag.Articles = operation._articles;

                var rd = HttpContext.Request.RequestContext.RouteData;
                CurrentAction = rd.GetRequiredString("action");
                CurrentController = rd.GetRequiredString("controller");
                AbsoluteUrl = HttpContext.Request.Url.AbsolutePath;
                UrlParams = AbsoluteUrl.Substring(AbsoluteUrl.LastIndexOf('/') + 1);

                var operation2 = new LoadPageTextsOperation(tokenHash, CurrentAction, CurrentController);
                operation2.ExcecuteTransaction();
                ViewBag.PageTexts = operation2._pageTexts;

                var operation3 = new LoadSeoParamsOperation(tokenHash, CurrentAction, CurrentController, AbsoluteUrl, UrlParams);
                operation3.ExcecuteTransaction();
                ViewBag.SeoParams = operation3._seoParams;
            }
        }
    }
}