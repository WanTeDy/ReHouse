#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.BusinessOperations.Home;
using ReHouse.Utils.Helpers;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.Feedbacks;
using ReHouse.Utils;
using System.Threading;

namespace ReHouse.FrontEnd.Controllers
{
    public class VacancyController : Controller
    {        
        public ActionResult Index()
        {                  
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;

            //var operation = new LoadFeedbacksOperation(tokenHash, 1, ConstV.ItemsPerPage);
            //operation.ExcecuteTransaction();
            return View();// operation._userFeedbacks);
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