using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReHouse.FrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
                "TagPage",
                "Special/{action}/{id}",
                new { controller = "Special", id = UrlParameter.Optional },
                new[] { "ReHouse.FrontEnd.Controllers" }
                );

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "ReHouse.FrontEnd.Controllers" }
                );
        }
    }
}
