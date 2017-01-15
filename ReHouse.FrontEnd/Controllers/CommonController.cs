#define CASH

using System;
using System.Web.Mvc;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Except;
using ITfamily.Utils.WebApi.Facade;
using ITfamily.Utils.WebApi.Facade.Brain;
using ITFamily.FrontEnd.Helpers;
using ITFamily.FrontEnd.Models;

namespace ITFamily.FrontEnd.Controllers
{
    public class CommonController : Controller
    {
        //Text page
        public ActionResult Delivery()
        {
            ViewBag.Route = RouteEnum.CommonDelivery;
            var model = new HelperCatalog();
            var res = CarouselFacade.GetCarousels().Result;
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success && res.CarouselModels != null)
            {
                ViewBag.CarouselModels = res.CarouselModels;
            }
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
            return View(model);
        }
        //Text page
        public ActionResult Warranty()
        {
            ViewBag.Route = RouteEnum.CommonWarranty;
            var model = new HelperCatalog();
            var res = CarouselFacade.GetCarousels().Result;
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success && res.CarouselModels != null)
            {
                ViewBag.CarouselModels = res.CarouselModels;
            }
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
            return View(model);
        }

        //Text page
        public ActionResult Contacts()
        {
            ViewBag.Route = RouteEnum.CommonContacts;
            var model = new HelperCatalog();
            var res = CarouselFacade.GetCarousels().Result;
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success && res.CarouselModels != null)
            {
                ViewBag.CarouselModels = res.CarouselModels;
            }
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
            return View(model);
        }

        //Text page
        public ActionResult WorkWithUs()
        {
            ViewBag.Route = RouteEnum.CommonWorkWithUs;
            var model = new HelperCatalog();
            var res = CarouselFacade.GetCarousels().Result;
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success && res.CarouselModels != null)
            {
                ViewBag.CarouselModels = res.CarouselModels;
            }
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
            return View(model);
        }
        //Text page
        public ActionResult TermsOfUse()
        {
            ViewBag.Route = RouteEnum.CommonTermsOfUse;
            var model = new HelperCatalog();
            var res = CarouselFacade.GetCarousels().Result;
            if (res != null && res.ErrorCode == (int)ErrorCodes.Success && res.CarouselModels != null)
            {
                ViewBag.CarouselModels = res.CarouselModels;
            }
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
            return View(model);
        }

        public ActionResult Journal(JournalModel model)
        {
            ViewBag.Route = RouteEnum.CommonJournalClient;
            var token = "";
            var session = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (session != null)
            {
                token = session.TokenHash;
            }

            var journal = JournalOrderFacade.GetJournalOrdersWithDates(token, OrderType.AllOrders, model.From, model.To, false).Result;
            if (journal != null && journal.ErrorCode == (int)ErrorCodes.Success)
            {
                SessionHelpers.Session("journal", journal.OrderComesModels);
                return PartialView("Partial/Profile/_JournalOrders", journal.OrderComesModels);
            }
            else
                return PartialView("Partial/Profile/_JournalOrders");
        }
        //JournalPartner
        public ActionResult JournalPartner(JournalModel model)
        {
            ViewBag.Route = RouteEnum.CommonJournalPartnerPartial;
            var token = "";
            var session = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
            if (session != null)
            {
                token = session.TokenHash;
            }

            var journal = JournalOrderFacade.GetJournalOrdersWithDates(token, OrderType.AllOrders, model.From, model.To, false).Result;
            if (journal != null && journal.ErrorCode == (int)ErrorCodes.Success)
            {
                SessionHelpers.Session("journal", journal.OrderComesModels);
                return PartialView("PartialCorporative/_JournalPartner", journal.OrderComesModels);
            }
            else
                return PartialView("PartialCorporative/_JournalPartner");
        }
        [HttpPost]
        public ActionResult FeedbackPost(FeedbackModel m)
        {
            ViewBag.Route = RouteEnum.CommonFeedbackPartial;
            try
            {
                if (ModelState.IsValid)
                {
                    var res = FeedbackFacade.AddFeedback(m.Message, m.Email, m.FirstName).Result;
                    if (res != null && res.ErrorCode == (int) ErrorCodes.Success)
                    {
                        m.Email = "";
                        m.FirstName = "";
                        m.Message = "";
                        Response.AppendHeader("FeedbackResponse", "Success");
                    }
                    else if (res != null && res.ErrorCode != (int)ErrorCodes.Success)
                    {
                        ModelState.AddModelError("Error", res.ExceptionMessage);
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Попробуйте чуть позже, извените сейчас сервер не доступен.");
                    }
                }
                return PartialView("_FeedbackPartial", m);
            }
            catch (Exception ex)
            {
                Response.AppendHeader("Error", ex.Message);
                ModelState.AddModelError("Error", ex.Message);
                return PartialView("_FeedbackPartial", m);
            }
        }
    }
}