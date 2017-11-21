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
using ReHouse.Utils.BusinessOperations.AdminFeedbacks;
using ReHouse.Utils;

namespace ReHouse.FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        private const Int32 _articlesCount = 2;
        private const Int32 _feedbacksCount = 3;

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

                if (CurrentController == "rent" || CurrentController == "sale" || CurrentController == "newbuilding" || (CurrentController == "home" && CurrentAction == "index"))
                {
                    var operation = new LoadArticlesOperation(TokenHash, 1, _articlesCount);
                    operation.ExcecuteTransaction();
                    ViewBag.Articles = operation._articles;

                    var operation2 = new LoadAdminFeedbacksOperation(TokenHash, 1, _feedbacksCount);
                    operation2.ExcecuteTransaction();
                    ViewBag.Feedbacks = operation2._feedbacks;

                    var operation4 = new LoadPartnersOperation(TokenHash, 1, ConstV.ItemsPerPage);
                    operation4.ExcecuteTransaction();
                    ViewBag.Partners = operation4._partners;
                }
                if (CurrentController == "rent" || CurrentController == "sale")
                    ViewBag.Phones = new string[] { "048 788 67 07" };
                else if (CurrentController == "newbuilding")
                    ViewBag.Phones = new string[] { "048 796 46 43" };
                else
                    ViewBag.Phones = new string[] { "048 788 67 07", "048 796 46 43" };
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