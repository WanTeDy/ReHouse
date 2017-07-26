using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.Feedbacks;
using ReHouse.Utils.BusinessOperations.Seo;
using ReHouse.Utils.BusinessOperations.TagPages;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class TagPagesController : BaseCabinetController
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadTagPagesOperation(sessionModel.TokenHash, Utils.Helpers.AdvertsType.All, 0);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._tagPages == null || operation._tagPages.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._tagPages);
        }

        //[HttpPost]
        //public ActionResult Load(PageModel page)
        //{
        //    if (!SessionHelpers.IsAuthentificated())
        //        return Redirect("/");
        //    var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
        //    if (page.PageNumber < 1)
        //        return Json(new { noElements = true });

        //    var operation = new LoadFeedbacksOperation(sessionModel.TokenHash, page.PageNumber, ConstV.ItemsPerPageAdmin, true);
        //    operation.ExcecuteTransaction();
        //    if (operation._userFeedbacks == null || operation._userFeedbacks.Count == 0)
        //        return Json(new { noElements = true });
        //    return PartialView("Feedback/_listOfFeedbacks", operation._userFeedbacks);
        //}

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            int Id = id ?? 0;

            if (Id == 0)
                return HttpNotFound();
            var operation = new LoadTagPageByIdOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();           

            if (operation._tagPage == null)
                return HttpNotFound();

            var op6 = new LoadSeoParamOperation(sessionModel.TokenHash, ConstV.DetailAction, CurrentController, "/" + CurrentController + "/" + ConstV.DetailAction + "/" + operation._tagPage.ShortName, operation._tagPage.ShortName, true);
            op6.ExcecuteTransaction();
            ViewBag.SeoParam = op6._seoParams ?? new SeoParam();

            return View(operation._tagPage);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(TagPage model, string seo_title, string seo_description, string seo_keywords, int seo_id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateTagPageOperation(sessionModel.TokenHash, model);
            operation.ExcecuteTransaction();

            var seoparam = new SeoParam
            {
                Id = seo_id,
                ActionName = ConstV.DetailAction,
                ControllerName = CurrentController,
                Description = seo_description,
                Keywords = seo_keywords,
                Title = seo_title,
                UrlParams = model.ShortName,
                FullUrl = "/" + CurrentController + "/" + ConstV.DetailAction + "/" + model.ShortName,
            };
            ViewBag.SeoParam = seoparam;

            var operation2 = new UpdateSeoParamOperation(sessionModel.TokenHash, seoparam);
            operation2.ExcecuteTransaction();

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }            
            return RedirectToAction("List");
        }

        //[HttpPost]
        //public ActionResult Delete(int[] buildersId)
        //{
        //    if (!SessionHelpers.IsAuthentificated())
        //        return Redirect("/");

        //    var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
        //    var op = new DeleteFeedbackOperation(sessionModel.TokenHash, buildersId);
        //    op.ExcecuteTransaction();

        //    var operation = new LoadFeedbacksOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin, true);
        //    operation.ExcecuteTransaction();
        //    if (operation._userFeedbacks == null || operation._userFeedbacks.Count == 0)
        //        return Json(new { noElements = true });
        //    return PartialView("Feedback/_listOfFeedbacks", operation._userFeedbacks);
        //}        
    }
}