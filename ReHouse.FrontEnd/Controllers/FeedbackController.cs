#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.BusinessOperations.Home;
using ReHouse.Utils.Helpers;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.Feedbacks;
using ReHouse.Utils;
using System.Threading;

namespace ReHouse.FrontEnd.Controllers
{
    public class FeedbackController : BaseController
    {        
        public ActionResult Index()
        {                  
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            
            var operation = new LoadFeedbacksOperation(tokenHash, 1, ConstV.ItemsPerPage);
            operation.ExcecuteTransaction();
            ViewBag.Feedbacks = operation._userFeedbacks;
            ViewBag.NotShowNews = true;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(UserFeedback model)
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;

            var operation = new AddFeedbackOperation(tokenHash, model.Username, model.Description);
            operation.ExcecuteTransaction();
            if(!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Load(PageModel page)
        {
            if (page.PageNumber < 1)
                return Json(new { noElements = true });
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadFeedbacksOperation(tokenHash, page.PageNumber, ConstV.ItemsPerPage);
            operation.ExcecuteTransaction();
            if (operation._userFeedbacks == null || operation._userFeedbacks.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Feedback/_listOfFeedbacks", operation._userFeedbacks);
        }
    }
}