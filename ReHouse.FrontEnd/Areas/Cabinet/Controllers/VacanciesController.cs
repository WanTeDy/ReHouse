using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.Vacancies;
using ReHouse.Utils.DataBase.Vacancies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class VacanciesController : Controller
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadVacanciesOperation(sessionModel.TokenHash);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._vacancies == null || operation._vacancies.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._vacancies);
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
            var operation = new LoadVacancyOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();
            if (operation._vacancy == null)
                return HttpNotFound();

            return View(operation._vacancy);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Vacancy model)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateVacancyOperation(sessionModel.TokenHash, model);
            operation.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }            
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int[] vacanciesId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var op = new DeleteVacancyOperation(sessionModel.TokenHash, vacanciesId);
            op.ExcecuteTransaction();

            var operation = new LoadVacanciesOperation(sessionModel.TokenHash);
            operation.ExcecuteTransaction();
            if (operation._vacancies == null || operation._vacancies.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Vacancy/_listOfVacancies", operation._vacancies);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;            
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Vacancy model, HttpPostedFileBase image)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddVacancyOperation(sessionModel.TokenHash, model);
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