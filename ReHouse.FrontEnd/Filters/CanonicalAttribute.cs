using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReHouse.FrontEnd.Models;


namespace ReHouse.FrontEnd.Filters
{
    public class CanonicalAttribute : ActionFilterAttribute
    {
        public string Url { get; private set; }

        public CanonicalAttribute(string url)
        {
            Url = url;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string fullyQualifiedUrl = "http://rehouse-realty.com.ua/" + this.Url;
            filterContext.Controller.ViewBag.CanonicalUrl = fullyQualifiedUrl;
            base.OnResultExecuting(filterContext);
        }
    }
}