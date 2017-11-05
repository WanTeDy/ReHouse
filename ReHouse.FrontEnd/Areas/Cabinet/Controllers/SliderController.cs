using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.Slider;
using ReHouse.Utils.BusinessOperations.Vacancies;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Vacancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class SliderController : Controller
    {
        [HttpGet]
        public ActionResult Edit()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var operation = new LoadSliderOptionsOperation(sessionModel.TokenHash);
            operation.ExcecuteTransaction();
            if (operation._params == null)
                return HttpNotFound();

            return View(operation._params);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(SliderParam[] Param)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateSliderOperation(sessionModel.TokenHash, Param.ToList());
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(Param.ToList());
            }
            return View(Param.ToList());
        }
    }
}