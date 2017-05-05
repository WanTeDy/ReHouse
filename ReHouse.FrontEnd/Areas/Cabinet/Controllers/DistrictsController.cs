using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.Districts;
using ReHouse.Utils.DataBase.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class DistrictsController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadDistrictsOperation(sessionModel.TokenHash);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._districts == null || operation._districts.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._districts);
        }   

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            int Id = id ?? 0;

            if (Id == 0)
                return HttpNotFound();
            var operation = new LoadDistrictOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();
            if (operation._district == null)
                return HttpNotFound();
            var operation2 = new LoadDistrictsOperation(sessionModel.TokenHash);
            operation2.ExcecuteTransaction();
            ViewBag.Districts = operation2._districts;

            return View(operation._district);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(District model)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateDistrictOperation(sessionModel.TokenHash, model);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                var operation2 = new LoadDistrictsOperation(sessionModel.TokenHash);
                operation2.ExcecuteTransaction();
                ViewBag.Districts = operation2._districts;

                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }            
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int[] districtsId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var op = new DeleteDistrictOperation(sessionModel.TokenHash, districtsId);
            op.ExcecuteTransaction();

            var operation = new LoadDistrictsOperation(sessionModel.TokenHash);
            operation.ExcecuteTransaction();
            if (operation._districts == null || operation._districts.Count == 0)
                return Json(new { noElements = true });
            return PartialView("District/_listOfDistricts", operation._districts);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var operation2 = new LoadDistrictsOperation(sessionModel.TokenHash);
            operation2.ExcecuteTransaction();
            ViewBag.Districts = operation2._districts;

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(District model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddDistrictOperation(sessionModel.TokenHash, model);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                var operation2 = new LoadDistrictsOperation(sessionModel.TokenHash);
                operation2.ExcecuteTransaction();
                ViewBag.Districts = operation2._districts;

                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }
            return RedirectToAction("List");
        }
    }
}