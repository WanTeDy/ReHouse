using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.Builders;
using ReHouse.Utils.DataBase.AdvertParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class BuildersController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadBuildersOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._builders == null || operation._builders.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._builders);
        }

        [HttpPost]
        public ActionResult Load(PageModel page)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (page.PageNumber < 1)
                return Json(new { noElements = true });

            var operation = new LoadBuildersOperation(sessionModel.TokenHash, page.PageNumber, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._builders == null || operation._builders.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Builder/_listOfBuilders", operation._builders);
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
            var operation = new LoadBuilderOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();
            if (operation._builder == null)
                return HttpNotFound();

            return View(operation._builder);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Builder model)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateBuilderOperation(sessionModel.TokenHash, model);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }            
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int[] buildersId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var op = new DeleteBuilderOperation(sessionModel.TokenHash, buildersId);
            op.ExcecuteTransaction();

            var operation = new LoadBuildersOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._builders == null || operation._builders.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Builder/_listOfBuilders", operation._builders);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;            
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Builder model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddBuilderOperation(sessionModel.TokenHash, model);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }
            return RedirectToAction("List");
        }
    }
}