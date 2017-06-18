using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.Feedbacks;
using ReHouse.Utils.BusinessOperations.Seo;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class TextsController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadPageTextsOperation(sessionModel.TokenHash);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._texts == null || operation._texts.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._texts);
        }

        //[HttpPost]
        //public ActionResult Load(PageModel page)
        //{
        //    if (!SessionHelpers.IsAuthentificated())
        //        return Redirect("/");
        //    var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
        //    if (page.PageNumber < 1)
        //        return Json(new { noElements = true });

        //    var operation = new LoadFeedbacksOperation(sessionModel.TokenHash, page.PageNumber, ConstV.ItemsPerPageAdmin, true);
        //    operation.ExcecuteTransaction();
        //    if (operation._userFeedbacks == null || operation._userFeedbacks.Count == 0)
        //        return Json(new { noElements = true });
        //    return PartialView("Feedback/_listOfFeedbacks", operation._userFeedbacks);
        //}

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            int Id = id ?? 0;

            if (Id == 0)
                return HttpNotFound();
            var operation = new LoadPageTextByIdOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();
            if (operation._text == null)
                return HttpNotFound();

            return View(operation._text);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PageText model)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdatePageTextOperation(sessionModel.TokenHash, model);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }            
            return RedirectToAction("List");
        }

        //[HttpPost]
        //public ActionResult Delete(int[] buildersId)
        //{
        //    if (!SessionHelpers.IsAuthentificated())
        //        return Redirect("/");

        //    var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
        //    var op = new DeleteFeedbackOperation(sessionModel.TokenHash, buildersId);
        //    op.ExcecuteTransaction();

        //    var operation = new LoadFeedbacksOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, true);
        //    operation.ExcecuteTransaction();
        //    if (operation._userFeedbacks == null || operation._userFeedbacks.Count == 0)
        //        return Json(new { noElements = true });
        //    return PartialView("Feedback/_listOfFeedbacks", operation._userFeedbacks);
        //}        
    }
}