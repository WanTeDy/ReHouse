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

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class BaseCabinetController : Controller
    {
        public string CurrentAction { get; set; }
        public string CurrentController { get; set; }
        public string AbsoluteUrl { get; set; }
        public string UrlParams { get; set; }
        
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var rd = HttpContext.Request.RequestContext.RouteData;
            CurrentAction = rd.GetRequiredString("action");
            CurrentController = rd.GetRequiredString("controller");
            AbsoluteUrl = HttpContext.Request.Url.AbsolutePath;
            UrlParams = AbsoluteUrl.Substring(AbsoluteUrl.LastIndexOf('/') + 1);
        }
    }
}