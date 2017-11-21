using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class PartnersController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadPartnersOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();

            ViewBag.NoElements = false;
            if (operation._partners == null || operation._partners.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._partners);
        }

        [HttpPost]
        public ActionResult Delete(int[] partnersId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var op = new DeletePartnerOperation(sessionModel.TokenHash, partnersId);
            op.ExcecuteTransaction();

            var operation = new LoadPartnersOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._partners == null || operation._partners.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Partner/_listOfPartners", operation._partners);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddPartnerOperation(sessionModel.TokenHash, image);
            operation.ExcecuteTransaction();

            return RedirectToAction("List");
        }
    }
}