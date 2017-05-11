using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils.BusinessOperations.Users;
using ReHouse.Utils.DataBase.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Cabinet/Profile
        public ActionResult Index()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var op = new LoadUserOperation(sessionModel.TokenHash, sessionModel.User.Id);
            op.ExcecuteTransaction();
            if (op.Success)
                return View(op._user);

            return HttpNotFound();
        }

        public ActionResult Edit()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var op = new LoadUserOperation(sessionModel.TokenHash, sessionModel.User.Id);
            op.ExcecuteTransaction();
            if (op.Success)
                return View(op._user);

            return HttpNotFound();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(User model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            var op = new UpdateUserOperation(model, image, sessionModel.TokenHash);
            op.ExcecuteTransaction();
            if (!op.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, op.Errors);
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}