#define CASH

using System;
using System.Linq;
using System.Web.Mvc;
using ITfamily.Utils;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;
using ITfamily.Utils.Helpers;
using ITfamily.Utils.WebApi.Facade;
using ITfamily.Utils.WebApi.Facade.Brain;
using ITFamily.FrontEnd.Helpers;
using ITFamily.FrontEnd.Models;

namespace ITFamily.FrontEnd.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Basket()
        {
            ViewBag.Route = RouteEnum.CartBasket;
            var model = new HelperCatalog();
            var token = "";
            var session = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (session != null) token = session.TokenHash;
#if(CASH)
            var categories = Singl.GetCategories(token);
            if (categories == null) return View(model);
            model.SelecedCategory.ItFamilyCategories = categories;
#else
            var res = BrainLoadFacade.LoadBrainCategories(false).Result;
            if (res == null || res.ErrorCode != (int)ErrorCodes.Success) return View(model);
            model.SelecedCategory.BrainCategories = res.BrainCategories;
#endif


            //get basket from session if token = null || session user == null
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (sessionModel != null)
            {
                var r = OrderComesFacade.GetOrderComesProductModel(token, false).Result;
                if (r != null && r.ErrorCode == (int)ErrorCodes.Success)
                {
                    if (sessionModel.RoleName == ConstV.RolePartner)
                    {
                        sessionModel.AmountUah = r.AmountUah;
                        sessionModel.AmountUsd = r.AmountUsd;
                        SessionHelpers.Session("user", sessionModel);
                    }
                    if (r.Basket != null && r.Basket.Count > 0)
                    {
                        model.Basket = r.Basket;
                    }
                    SessionHelpers.Session("CountProducts", r.CountPrepaidOrder);
                    ViewBag.OrderCities = r.OrderCitieses;
                }
            }

            return View(model);
        }

        public ActionResult AddToCart(int productId, int categoryId, int page = 1, int quantity = 1)
        {
            //todo add to session

            //var res = BrainLoadFacade.LoadBrainCategories(false).Result;
            //var model = new HelperCatalog();
            //if (res == null || res.ErrorCode != (int)ErrorCodes.Success)
            //    return RedirectToAction("Products", "Catalog", new { categoryId, page });
            //model.SelecedCategory.BrainCategories = res.BrainCategories;
            //model.SelecedCategory.CategoryId = categoryId;
            //model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(res.BrainCategories, categoryId);

            var token = "";
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (sessionModel != null) token = sessionModel.TokenHash;
            if (!String.IsNullOrEmpty(token))
            {
                var data = new DataPostOrder
                {
                    productID = productId,
                    quantity = quantity
                };
                var r = OrderComesFacade.AddProductToOrder(data, token, false).Result;
                if (r != null && r.ErrorCode == (int)ErrorCodes.Success)
                {
                    SessionHelpers.Session("CountProducts", r.CountPrepaidOrder);
                    if (sessionModel != null && sessionModel.RoleName == ConstV.RolePartner)
                    {
                        sessionModel.AmountUah = r.AmountUah;
                        sessionModel.AmountUsd = r.AmountUsd;
                        SessionHelpers.Session("user", sessionModel);
                    }
                }
            }
            return RedirectToAction("Products", "Catalog", new { categoryId, page });
        }

        public ActionResult BuyGood(int productId, int categoryId, int page = 1, int quantity = 1)
        {
            //todo add to session

            //var res = BrainLoadFacade.LoadBrainCategories(false).Result;
            //var model = new HelperCatalog();
            //if (res == null || res.ErrorCode != (int)ErrorCodes.Success)
            //    return RedirectToAction("Products", "Catalog", new { categoryId, page });
            //model.SelecedCategory.BrainCategories = res.BrainCategories;
            //model.SelecedCategory.CategoryId = categoryId;
            //model.SelecedCategory.CategoryModel = Helper.GetActiveCategoryModel(res.BrainCategories, categoryId);

            var token = "";
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (sessionModel != null) token = sessionModel.TokenHash;
            if (!String.IsNullOrEmpty(token))
            {
                var data = new DataPostOrder
                {
                    productID = productId,
                    quantity = quantity
                };
                var r = OrderComesFacade.AddProductToOrder(data, token, false).Result;
                if (r != null && r.ErrorCode == (int)ErrorCodes.Success)
                {
                    SessionHelpers.Session("CountProducts", r.CountPrepaidOrder);
                    if (sessionModel != null && sessionModel.RoleName == ConstV.RolePartner)
                    {
                        sessionModel.AmountUah = r.AmountUah;
                        sessionModel.AmountUsd = r.AmountUsd;
                        SessionHelpers.Session("user", sessionModel);
                    }
                    return RedirectToAction("Basket", "Cart");
                }
            }
            return RedirectToAction("Products", "Catalog", new { categoryId, page });
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (sessionModel != null)
            {
                var token = sessionModel.TokenHash;
                if (!String.IsNullOrEmpty(token))
                {
                    var r = OrderComesFacade.DeleteProductFromOrder(productId, token, false).Result;
                    if (r != null && r.ErrorCode == (int)ErrorCodes.Success)
                    {
                        SessionHelpers.Session("CountProducts", r.CountPrepaidOrder);
                        if (sessionModel.RoleName == ConstV.RolePartner)
                        {
                            sessionModel.AmountUah = r.AmountUah;
                            sessionModel.AmountUsd = r.AmountUsd;
                            SessionHelpers.Session("user", sessionModel);
                        }
                    }
                }
            }

            return RedirectToAction("Basket", "Cart");
        }
        [HttpPost]
        public ActionResult ChangeQuantity(int productId, int quantity)
        {
            var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (sessionModel != null)
            {
                var token = sessionModel.TokenHash;
                if (!String.IsNullOrEmpty(token))
                {
                    var r = OrderComesFacade.ChangeQuantity(productId, quantity, token, false).Result;
                    if (r != null && r.ErrorCode == (int)ErrorCodes.Success)
                    {
                        SessionHelpers.Session("CountProducts", r.CountPrepaidOrder);
                        if (sessionModel.RoleName == ConstV.RolePartner)
                        {
                            sessionModel.AmountUah = r.AmountUah;
                            sessionModel.AmountUsd = r.AmountUsd;
                            SessionHelpers.Session("user", sessionModel);
                        }
                    }
                }
            }

            return RedirectToAction("Basket", "Cart");
        }

        private void SetViewBagCities()
        {
#if(CASH)
            var cities = Singl.GetOrderCities();
            if (cities != null)
                ViewBag.OrderCities = cities;
#else
            var resCities = CommonFacade.OrderCities().Result;
            if (resCities == null || resCities.ErrorCode != (int)ErrorCodes.Success) return;
            ViewBag.OrderCities = resCities.OrderCities;
#endif
        }
        [HttpPost]
        public ActionResult OrderGoods(OrderGoodsModel m)
        {
            ViewBag.Route = RouteEnum.CartBasketOrderGoods;
            try
            {
                if (ModelState.IsValid)
                {
                    var sessionModel = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
                    if (sessionModel != null)
                    {
                        var token = sessionModel.TokenHash;
                        if (!String.IsNullOrEmpty(token))
                        {
                            var adress = m.DeliveryCity + " " + m.DeliveryAdress;
                            var r =
                                OrderComesFacade.OrderGoodsForClient(token, ConstV.DeliveryTypesFromString[m.DeliveryType],
                                    ConstV.PaymentMethodsFromString[m.PaymentMetod], m.Comment, adress, m.Email, m.FIO,
                                    m.Phone, false).Result;
                            if (r != null && r.ErrorCode == (int)ErrorCodes.Success)
                            {
                                SessionHelpers.Session("CountProducts", 0);
                                if (sessionModel.RoleName == ConstV.RolePartner)
                                {
                                    sessionModel.AmountUah = 0;
                                    sessionModel.AmountUsd = 0;
                                    SessionHelpers.Session("user", sessionModel);
                                }
                                var @from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                var journal = JournalOrderFacade.GetJournalOrdersWithDates(token, OrderType.AllOrders, @from, DateTime.Now, false).Result;
                                if (journal != null && journal.ErrorCode == (int)ErrorCodes.Success)
                                {
                                    SessionHelpers.Session("journal", journal.OrderComesModels);
                                }
                                Response.AppendHeader("OrderGoods", "Success");
                                var url = Url.Action("Index", "Home");
                                if (url != null) Response.AppendHeader("Url", url);
                            }
                            else if (r != null && r.ErrorCode != (int)ErrorCodes.Success)
                            {
                                ModelState.AddModelError("Error", r.ExceptionMessage);
                                SetViewBagCities();
                            }
                            else
                            {
                                ModelState.AddModelError("Error", "Попробуйте чуть позже, извените сейчас сервер не доступен.");
                                SetViewBagCities();
                            }
                        }
                    }
                }
                return PartialView("Partial/_OrderGoods", m);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                Response.AppendHeader("Error", ex.Message);
                SetViewBagCities();
                return PartialView("Partial/_OrderGoods", m);
            }
        }
    }
}