using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.AdminFeedbacks;
using ReHouse.Utils.DataBase.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class AdminFeedbacksController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadAdminFeedbacksOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._feedbacks == null || operation._feedbacks.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._feedbacks);
        }

        [HttpPost]
        public ActionResult Load(PageModel page)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (page.PageNumber < 1)
                return Json(new { noElements = true });

            var operation = new LoadAdminFeedbacksOperation(sessionModel.TokenHash, page.PageNumber, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._feedbacks == null || operation._feedbacks.Count == 0)
                return Json(new { noElements = true });
            return PartialView("AdminFeedback/_listOfFeedbacks", operation._feedbacks);
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
            var operation = new LoadAdminFeedbackOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();
            if (operation._feedback == null)
                return HttpNotFound();

            return View(operation._feedback);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(AdminFeedback model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateAdminFeedbackOperation(model, image, sessionModel.TokenHash);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }            
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int[] feedbacksId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var op = new DeleteAdminFeedbackOperation(sessionModel.TokenHash, feedbacksId);
            op.ExcecuteTransaction();

            var operation = new LoadAdminFeedbacksOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._feedbacks == null || operation._feedbacks.Count == 0)
                return Json(new { noElements = true });
            return PartialView("AdminFeedback/_listOfFeedbacks", operation._feedbacks);
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
        public ActionResult Add(AdminFeedback model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddAdminFeedbackOperation(model, image, sessionModel.TokenHash);
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