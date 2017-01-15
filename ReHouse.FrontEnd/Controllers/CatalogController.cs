#define CASH

using System.Linq;
using System.Web.Mvc;
using ITfamily.Utils.BusinessOperations;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;
using ITfamily.Utils.Helpers;
using ITfamily.Utils.WebApi.Facade;
using ITfamily.Utils.WebApi.Facade.Brain;
using ITFamily.FrontEnd.Helpers;
using ITFamily.FrontEnd.Models;

namespace ITFamily.FrontEnd.Controllers
{
    public class CatalogController : Controller
    {
        public ActionResult Products(int categoryId, int page = 1)
        {
            ViewBag.Route = RouteEnum.CatalogProducts;
            var res = CarouselFacade.GetCarousels().Result;
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success && res.CarouselModels != null)
            {
                ViewBag.CarouselModels = res.CarouselModels;
            }
            Session["Page"] = page;
            if (!(Session["ItemsPerPage"] is int)) Session["ItemsPerPage"] = 20;
            var items = (int)Session["ItemsPerPage"];
            var model = new HelperCatalog {SelecedCategory = {CategoryId = categoryId}};
            var token = "";
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (sessionModel != null) token = sessionModel.TokenHash;
#if(CASH)
            var categories = Singl.GetCategories(token);
            if (categories == null) return View(model);
            model.SelecedCategory.ItFamilyCategories = categories;
            model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(categories, categoryId);
#else
            var res = BrainLoadFacade.LoadBrainCategories(false).Result;
            if (res == null || res.ErrorCode != (int)ErrorCodes.Success) return View(model);
            model.SelecedCategory.BrainCategories = res.BrainCategories;
            model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(res.BrainCategories, categoryId);
#endif
            var resProd = BrainLoadFacade.LoadProductsWithPaginationForSite(categoryId, page, items, token, false, true).Result;
            if (resProd == null || resProd.BrainProductModels == null) return View(model);
            Session["CountPages"] = resProd.CountPages;
            Session["TotalItems"] = resProd.TotalItems;
            model.PagingInfo = new PagingInfo
            {
                ItemsPerPage = items,
                CurrentPage = page,
                TotalItems = resProd.TotalItems
            };
            model.BrainProductModels = resProd.BrainProductModels;
            
            return View(model);
        }

        public ActionResult Search(string searchName, int categoryId =0)
        {
            ViewBag.Route = RouteEnum.CatalogSearch;
            var token = "";
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (sessionModel != null) token = sessionModel.TokenHash;
            var res = BrainLoadFacade.SearchProducts(token, searchName, categoryId, false).Result;
            var model = new HelperCatalog();
            if (res == null || res.ErrorCode != (int)ErrorCodes.Success) return RedirectToAction("Index","Home");
            model.CategorySearchModel = res.CategorySearchModels;
            model.SearchName = res.SearchName;
            model.SelecedCategory.CategoryId = categoryId;
            //model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(res.BrainCategories, categoryId);
            model.BrainProductModels = res.BrainProductModels;
            
            return View(model);
        }
        public ActionResult ReviewGood(int productId, int categoryId)
        {
            ViewBag.Route = RouteEnum.CatalogReviewGood;
            var page = 1;
            if (Session["page"] is int)
                page = (int) Session["page"];
            var model = new HelperCatalog {SelecedCategory = {CategoryId = categoryId}};
            var token = "";
            var sessionModel = Session["user"] as SessionModel;
            if (sessionModel != null) token = sessionModel.TokenHash;
#if(CASH)
            var categories = Singl.GetCategories(token);
            if (categories == null) return RedirectToAction("Products", "Catalog", new { categoryId, page });
            model.SelecedCategory.ItFamilyCategories = categories;
            model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(categories, categoryId);
#else
            var res = BrainLoadFacade.LoadBrainCategories(false).Result;
            if (res == null || res.ErrorCode != (int)ErrorCodes.Success) return RedirectToAction("Products", "Catalog", new { categoryId, page });
            model.SelecedCategory.BrainCategories = res.BrainCategories;
            model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(res.BrainCategories, categoryId);
#endif
            var resProd = BrainLoadFacade.LoadBrainProductForReviewGood(token, productId, false, false).Result;
            if (resProd == null || resProd.BrainProductModel == null)
                return RedirectToAction("Products", "Catalog", new { categoryId, page });

            model.Product = resProd.BrainProductModel;
            ViewBag.Image = resProd.BrainProductModel.MediumImage;
            if (resProd.BrainProductFullInfo != null)
            {
                model.ProductFullInfo = resProd.BrainProductFullInfo;
                ViewBag.Description = resProd.BrainProductFullInfo.description;
            }
            ViewBag.OftenBuyProducts = resProd.BrainProductModels;
            //var r = CommonAccess.GetProductFromBrain("Kovalchuk", GenerateHash.GetMd5Hash("t5y6u7i81!"), model.Product.productID);
            //model.ProductFullInfo = r;
            //if (r != null)
            //  ViewBag.Description = r;
            return View(model);
        }

        public ActionResult ChangeItemsPerPage(int categoryId, int itemsPerPage)
        {
            Session["page"] = 1;
            if (itemsPerPage > 100)
            {
                Session["ItemsPerPage"] = 100;
            }
            else
            {
                Session["ItemsPerPage"] = itemsPerPage;
            }
            
            return RedirectToAction("Products", "Catalog", new { categoryId, page = 1 });
            
            //var res = BrainLoadFacade.LoadBrainCategories(false).Result;
            //var model = new HelperCatalog();
            //if (res == null || res.ErrorCode != (int)ErrorCodes.Success) return RedirectToAction("Products", "Catalog", new { categoryId, page = 1 });
            //model.SelecedCategory.BrainCategories = res.BrainCategories;
            //model.SelecedCategory.CategoryId = categoryId;
            //model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(res.BrainCategories, categoryId);
            //
            //var token = "";
            //var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            //if (sessionModel != null) token = sessionModel.TokenHash;
            //var resProd = BrainLoadFacade.LoadProductsPagination(categoryId, 1, itemsPerPage, token, false).Result;
            //if (resProd == null || resProd.BrainProductModels == null) return View(model);
            ////Session["CountPages"] = resProd.CountPages;
            ////Session["TotalItems"] = resProd.TotalItems;
            //model.PagingInfo = new PagingInfo
            //{
            //    ItemsPerPage = itemsPerPage,
            //    CurrentPage = 1,
            //    TotalItems = resProd.TotalItems
            //};
            //model.BrainProductModels = resProd.BrainProductModels;
            //
            //return View(model);
        }

        //public ActionResult ProductsCorporative()
        //{
        //    return View();
        //}
    }
}