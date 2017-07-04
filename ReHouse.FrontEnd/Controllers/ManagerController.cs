#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.Managers;

namespace ReHouse.FrontEnd.Controllers
{
    public class ManagerController : BaseController
    {        
        public ActionResult Index()
        {            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadManagersOperation(tokenHash);
            operation.ExcecuteTransaction();

            ViewBag.NotShowNews = true;
            return View(operation._users);
        }

        public new ActionResult Profile(int? id)
        {
            int Id = id ?? 0;
            if (Id == 0)
                return HttpNotFound();
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadManagerOperation(tokenHash, Id);
            operation.ExcecuteTransaction();
            ViewBag.NotShowNews = true;
            if(operation._user == null)
                return HttpNotFound();
            return View(operation._user);
        }
    }
}