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


namespace ReHouse.FrontEnd.Controllers
{
    public class FeedbackController : Controller
    {        
        public ActionResult Index()
        {            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var tokenHash = "";
            if (sessionModel != null)
                tokenHash = sessionModel.TokenHash;
            else
                SessionHelpers.Session("CountProducts", 0);
            //var operation = new LoadAdvertsForHomePageOperation(tokenHash);
            //operation.ExcecuteTransaction();
            //var model = new LoadAdvertsForHomePageModel
            //{
            //    HotAdverts = operation._hotAdverts,
            //    FlatSaleAdverts = operation._flatSaleAdverts,
            //    HouseSaleAdverts = operation._houseSaleAdverts,
            //    NewBuildingAdverts = operation._newBuildingAdverts,
            //};
            return View();
        }
    }
}