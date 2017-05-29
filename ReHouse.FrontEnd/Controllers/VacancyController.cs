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
using ReHouse.Utils.BusinessOperations.Vacancies;
using ReHouse.Utils;
using System.Threading;

namespace ReHouse.FrontEnd.Controllers
{
    public class VacancyController : BaseController
    {        
        public ActionResult Index()
        {                  
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;

            var operation = new LoadVacanciesOperation(tokenHash);
            operation.ExcecuteTransaction();
            return View(operation._vacancies);
        }        
    }
}