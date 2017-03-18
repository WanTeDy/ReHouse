using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class NewsController : Controller
    {
        // GET: Cabinet/News
        public ActionResult Index()
        {
            return View();
        }
    }
}