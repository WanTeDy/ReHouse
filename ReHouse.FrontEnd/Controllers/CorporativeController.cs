using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITfamily.Utils.Except;
using ITfamily.Utils.WebApi.Facade;
using ITfamily.Utils.WebApi.Facade.Brain;
using ITFamily.FrontEnd.Helpers;
using ITFamily.FrontEnd.Models;

namespace ITFamily.FrontEnd.Controllers
{
    public class CorporativeController : Controller
    {
        // GET: Corporative
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CorporativeBalans()
        {
            var token = "";
            var session = SessionHelpers.Session("user", typeof (SessionModel)) as SessionModel;
            if (session != null) token = session.TokenHash;
            var res = LoadFacade.GetFormedItfamilyCategories(true, false, token, false).Result;
            var model = new HelperCatalog();
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success)
                model.SelecedCategory.ItFamilyCategories = res.ItFamilyCategories;
            return View(model);
        }
        public ActionResult CorporativeProfils()
        {
            var token = "";
            var session = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (session != null) token = session.TokenHash;
            var res = LoadFacade.GetFormedItfamilyCategories(true, false, token, false).Result;
            var model = new HelperCatalog();
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success)
                model.SelecedCategory.ItFamilyCategories = res.ItFamilyCategories;
            return View(model);
        }
        public ActionResult CorporativeReports()
        {
            var token = "";
            var session = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (session != null) token = session.TokenHash;
            var res = LoadFacade.GetFormedItfamilyCategories(true, false, token, false).Result;
            var model = new HelperCatalog();
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success)
                model.SelecedCategory.ItFamilyCategories = res.ItFamilyCategories;
            return View(model);
        }
        public ActionResult CorporativeZakazy()
        {
            var token = "";
            var session = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (session != null) token = session.TokenHash;
            var res = LoadFacade.GetFormedItfamilyCategories(true, false, token, false).Result;
            var model = new HelperCatalog();
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success)
                model.SelecedCategory.ItFamilyCategories = res.ItFamilyCategories;
            return View(model);
        }
    }
}