﻿#define CASH

using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Services;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.BusinessOperations.Home;
using ReHouse.Utils.Helpers;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.News;
using ReHouse.Utils;

namespace ReHouse.FrontEnd.Controllers
{
    public class NewsController : Controller
    {        
        public ActionResult Index()
        {            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            var operation = new LoadArticlesOperation(tokenHash, 1, ConstV.ItemsPerPage);
            operation.ExcecuteTransaction();            
            return View(operation._articles);
        }
    }
}