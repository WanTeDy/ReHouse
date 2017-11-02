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
        public string TokenHash { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            TokenHash = "";
            if (sessionModel != null)
                TokenHash = sessionModel.TokenHash;
            if (HttpContext.Request.HttpMethod.ToLower() == "get")
            {
                var rd = HttpContext.Request.RequestContext.RouteData;
                CurrentAction = rd.GetRequiredString("action").ToLower();
                CurrentController = rd.GetRequiredString("controller").ToLower();
                AbsoluteUrl = HttpContext.Request.Url.AbsolutePath.ToLower();
                if (AbsoluteUrl.Count(x => x == '/') > 2)
                    UrlParams = AbsoluteUrl.Substring(AbsoluteUrl.LastIndexOf('/') + 1);

                var operation3 = new LoadSeoParamOperation(TokenHash, CurrentAction, CurrentController, AbsoluteUrl, UrlParams);
                operation3.ExcecuteTransaction();
                ViewBag.SeoParams = operation3._seoParams;

                if(CurrentController == "rent" || CurrentController == "sale" || CurrentController == "newbuilding" || (CurrentController == "home" && CurrentAction == "index"))
                {
                    var operation = new LoadArticlesOperation(TokenHash, 1, _articlesCount);
                    operation.ExcecuteTransaction();
                    ViewBag.Articles = operation._articles;
                }
            }
        }

        protected void LoadPageText()
        {
            var operation3 = new LoadPageTextOperation(TokenHash, CurrentAction, CurrentController, AbsoluteUrl, UrlParams);
            operation3.ExcecuteTransaction();
            ViewBag.PageTexts = operation3._pageTexts;
        }
    }
}