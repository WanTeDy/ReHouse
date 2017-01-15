using System.Web.Mvc;

namespace ITFamily.FrontEnd.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult InternalServerError()
        {
            return View();
        }
        public ActionResult ServiceUnavailable()
        {
            return View();
        }
    }
}