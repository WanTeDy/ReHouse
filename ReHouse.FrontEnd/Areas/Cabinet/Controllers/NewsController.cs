using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using ReHouse.Utils;
using ReHouse.Utils.BusinessOperations.News;
using ReHouse.Utils.BusinessOperations.Seo;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet.Controllers
{
    public class NewsController : BaseCabinetController
    {
        [HttpGet]
        public ActionResult List()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new LoadArticlesOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            
            ViewBag.NoElements = false;
            if (operation._articles == null || operation._articles.Count == 0)
                ViewBag.NoElements = true;
            return View(operation._articles);
        }
        
        [HttpPost]
        public ActionResult Load(PageModel page)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (page.PageNumber < 1)
                return Json(new { noElements = true });

            var operation = new LoadArticlesOperation(sessionModel.TokenHash, page.PageNumber, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._articles == null || operation._articles.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Article/_listOfArticles", operation._articles);
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
            var operation = new LoadArticleOperation(sessionModel.TokenHash, Id);
            operation.ExcecuteTransaction();

            var op6 = new LoadSeoParamOperation(sessionModel.TokenHash, ConstV.DetailAction, CurrentController, "/" + CurrentController + "/" + ConstV.DetailAction + "/" + operation._article.Id, operation._article.Id.ToString());
            op6.ExcecuteTransaction();
            ViewBag.SeoParam = op6._seoParams ?? new SeoParam();

            if (operation._article == null)
                return HttpNotFound();

            return View(operation._article);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Article model, HttpPostedFileBase image,
            string seo_title, string seo_description, string seo_keywords, int seo_id, Image imageData)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new UpdateArticleOperation(sessionModel.TokenHash, model.Id, model.Title, model.Description, image, imageData);
            operation.ExcecuteTransaction();

            var seoparam = new SeoParam
            {
                Id = seo_id,
                ActionName = ConstV.DetailAction,
                ControllerName = CurrentController,
                Description = seo_description,
                Keywords = seo_keywords,
                Title = seo_title,
                UrlParams = model.Id.ToString(),
                FullUrl = "/" + CurrentController + "/" + ConstV.DetailAction + "/" + model.Id,
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

        [HttpPost]
        public ActionResult Delete(int[] articlesId)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");

            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            
            var op = new DeleteArticleOperation(sessionModel.TokenHash, articlesId);
            op.ExcecuteTransaction();

            var operation = new LoadArticlesOperation(sessionModel.TokenHash, 1, ConstV.ItemsPerPageAdmin);
            operation.ExcecuteTransaction();
            if (operation._articles == null || operation._articles.Count == 0)
                return Json(new { noElements = true });
            return PartialView("Article/_listOfArticles", operation._articles);
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;            
            ViewBag.SeoParam = new SeoParam();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(Article model, HttpPostedFileBase image,
            string seo_title, string seo_description, string seo_keywords, int seo_id)
        {
            if (!SessionHelpers.IsAuthentificated())
                return Redirect("/");
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;

            var operation = new AddArticleOperation(sessionModel.TokenHash, model.Title, model.Description, image);
            operation.ExcecuteTransaction();

            var seoparam = new SeoParam
            {
                Id = seo_id,
                ActionName = ConstV.DetailAction,
                ControllerName = CurrentController,
                Description = seo_description,
                Keywords = seo_keywords,
                Title = seo_title,
                UrlParams = model.Id.ToString(),
                FullUrl = "/" + CurrentController + "/" + ConstV.DetailAction + "/" + operation._article.Id,
            };
            ViewBag.SeoParam = seoparam;

            var op6 = new AddSeoParamOperation(sessionModel.TokenHash, seoparam);

            if (!operation.Success)
            {
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
                return View(model);
            }
            return RedirectToAction("List");
        }
    }
}