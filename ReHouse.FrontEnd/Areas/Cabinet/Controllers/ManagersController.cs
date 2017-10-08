using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.Builders;
using ReHouse.Utils.BusinessOperations.Users;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class ManagersController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadUsersOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._users == null || operation._users.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._users);
        }

        [HttpPost]
        public ActionResult Load(PageModel page)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (page.PageNumber < 1)
                return Json(new { noElements = true });

            var operation = new LoadUsersOperation(sessionModel.TokenHash, page.PageNumber, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._users == null || operation._users.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Manager/_listOfManagers", operation._users);
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
            var operation = new LoadUserOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();
            if (operation._user == null)
                return HttpNotFound();

            return View(operation._user);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(User model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateUserOperation(model, image, sessionModel.TokenHash);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }            
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int[] managersId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var op = new DeleteUserOperation(sessionModel.TokenHash, managersId);
            op.ExcecuteTransaction();

            var operation = new LoadUsersOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._users == null || operation._users.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Manager/_listOfManagers", operation._users);
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
        public ActionResult Add(User model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddUserOperation(model, image, sessionModel.TokenHash);
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