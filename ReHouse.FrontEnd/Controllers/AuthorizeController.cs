#define CASH

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ReHouse.Utils.BusinessOperations;
using ReHouse.Utils.BusinessOperations.Users;
using ReHouse.Utils.BusinessOperations.Auth;
using ReHouse.Utils.DataBase.Security;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SendGrid;

namespace ReHouse.FrontEnd.Controllers
{
    public class AuthorizeController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "Email,Password")]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var pass = HashHelper.GetMd5Hash(model.Password);
                var operation = new CheckSignInDataOperation(model.Email, pass);
                operation.ExcecuteTransaction();
                if (operation.Success)
                {
                    var user = operation._user;
                    return SetSessionData(user);
                }
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
            }
            //TODO откорректировать вьюху
            return PartialView("UnregisteredUsers/_LoginPartial", model);

        }

        private ActionResult SetSessionData(User user)
        {
            var session = new SessionModel
            {
                User = user,
                Name = user.FirstName,
                FatherName = user.FatherName,
                SecondName = user.SecondName,
                Email = user.Email,
                Role = user.Role,
                TokenHash = user.TokenHash,
                Authorities = user.Role.Authorities,
                Phones = user.Phones,
            };
            SessionHelpers.Session("user", session);
            //return RedirectToAction("Index", "Home");
            return Json(new { url = Url.Action("Index", "Home") });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register([Bind(Include = "Name,Email,Password,ConfirmPassword")]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var pass = HashHelper.GetMd5Hash(model.Password);
                var operation = new AddUserOperation(model.Name, model.Email, pass, 1);
                operation.ExcecuteTransaction();
                if (operation.Success)
                {
                    var user = operation._user;
                    return SetSessionData(user);
                }
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
            }
            return PartialView("UnregisteredUsers/_registerPartial", model);

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RemindPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult RestorePasswordPartial(RestorePasswordModel m)
        {
            //check in db email and write token in db
            //generate url to request RecoveryPassword
            var operation = new RecoveryPasswordOperation(m.Email);
            operation.ExcecuteTransaction();
            if (operation.Success)
            {
                var user = operation._user;
                try
                {
                    SendMailAsync(user);
                    ViewBag.Success = true;
                }
                catch
                {
                    ModelState.AddModelError("Email", "Ошибка при отправлении письма");
                }
            }
            else
                ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
            return PartialView(m);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RecoveryPassword(string token)
        {
            //check token and add it's to session or hidden field
            var operation = new CheckTokenForAccessOperation(token);
            operation.ExcecuteTransaction();
            if (operation._access)
            {
                ViewBag.Token = operation._tokenHash;
                ViewBag.Success = operation._access;
            }
            else
            {
                ViewBag.Success = false;
                //TODO: Проверить че это за хрень
                ViewBag.Error = "Восстановление пароля неудачно";
            }
            //Подтверждение пароля
            return View();
        }

        //TODO: настроить емейл
        private void SendMailAsync(User user)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage { From = new MailAddress("admin@ReHouse.com.ua") };
            // Add the message properties.
            myMessage.AddTo(user.Email);

            myMessage.Subject = "Восстановление пароля для входа в аккаунт ReHouse.com.ua";
            var name = "";
            if (!String.IsNullOrEmpty(user.SecondName))
            {
                name = user.SecondName + " ";
            }
            if (!String.IsNullOrEmpty(user.FirstName))
            {
                name += user.FirstName + " ";
            }
            if (!String.IsNullOrEmpty(user.FatherName))
            {
                name += user.FatherName;
            }
            if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(user.Email))
            {
                name = user.Email;
            }
            var url = "http://www.ReHouse.com.ua/" + Url.Action("RecoveryPassword", "Authorize", new { token = user.TokenHash });

            string html = @"Здравствуйте, " + name + ".<br><br>Для пользователя " + "\"" + user.Email +
                          "\" был сделан запрос на восстановление пароля для входа на сайт. <br><br>Если вы не делали этого запроса, просто проигнорируйте это письмо.<br>В противном случае нажмите на ссылку ниже для подтверждения запроса и получения нового пароля.<br><br>------------------------------<wbr>------------------------------<br><a href=\"" +
                          url + "\" rel=\"noreferrer\" target=\"_blank\">" + url +
                          "</a><br>------------------------------<wbr>------------------------------<br><br><br>------------------------------<wbr>----------<br>Дата: " +
                          DateTime.Now.ToString("MM.dd.yyyy, HH:mm") +
                          "<br>------------------------------<wbr>----------<br><br>Всего наилучшего,<br> с уважением,<br>команда ReHouse.<br><br>"
                          + user.TokenHash;
            //Add the HTML and Text bodies
            myMessage.Html = html;

            //TODO: настройки почты
            var username = "azure_9835eac30489e63cb2a714af30cf2b07@azure.com";
            var pswd = "Ye7Id9s852aE3XK";

            var credentials = new NetworkCredential(username, pswd);
            // Create an Web transport for sending email, using credentials...
            var transportWeb = new Web(credentials);

            // ...OR create a Web transport, using API Key (preferred)
            //var transportWeb = new Web("This string is an API key");

            // Send the email.
            var r = transportWeb.DeliverAsync(myMessage);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult RecoveryPasswordPartial(RecoveryPasswordModel m)
        {
            if (m.Password == m.ConfirmPassword)
            {
                var pass = HashHelper.GetMd5Hash(m.Password);
                var operation = new SetPasswordOperation(pass, m.TokenHash);
                operation.ExcecuteTransaction();
                if (operation.Success)
                {
                    var user = operation._user;
                    ViewBag.Success = true;
                    return SetSessionData(user);
                }
                else
                    ErrorHelpers.AddModelErrors(ModelState, operation.Errors);
            }
            else
            {
                ModelState.AddModelError("Password", "Пароль не совпадают!");
                ViewBag.Success = false;
            }
            return PartialView(m);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            if (Session != null)
                Session.RemoveAll();
            SessionHelpers.SessionRemoveAll();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public ActionResult ChangeClientsData(ClientsDataModel m)
        //{
        //    var token = "";
        //    var session = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
        //    if (session != null)
        //    {
        //        token = session.TokenHash;
        //    }
        //    //Chekc data
        //    var contr = new Contractor
        //    {
        //        Id = session != null && session.Contractor != null ? session.Contractor.Id : 0,
        //        FatherName = m.FatherName,
        //        Email = m.Email,
        //        SecondName = m.SecondName,
        //        TokenHash = token,
        //        CreditLimit = session != null && session.Contractor != null ? session.CreditLimit : 0,
        //        FirstName = m.Name,
        //        FormOfTaxation = session != null && session.Contractor != null ? session.Contractor.FormOfTaxation : "",
        //        Ownership = session != null && session.Contractor != null ? session.Contractor.Ownership : "",
        //        Phone = m.Phone,
        //        Requisite = session != null && session.Contractor != null ? session.Contractor.Requisite : "",
        //        Url = session != null && session.Contractor != null ? session.Contractor.Url : "",
        //        TaxRate = session != null && session.Contractor != null ? session.Contractor.TaxRate : 0,
        //        DeliveryAdditional = m.DeliveryAdditional,
        //        DeliveryStreet = m.DeliveryStreet,
        //        DeliveryAppartament = m.DeliveryAppartament,
        //        DeliveryCity = m.DeliveryCity,
        //        DeliveryHome = m.DeliveryHome,
        //        //IsActive = session == null || session.Contractor.IsActive,
        //    };
        //    var res = ContractorFacade.UpdateClient(contr, token, false).Result;
        //    if (res != null && res.ErrorCode == (int)ErrorCodes.Success)
        //    {
        //        session = new SessionModel
        //        {
        //            Name = contr.FirstName,
        //            FatherName = contr.FatherName,
        //            SecondName = contr.SecondName,
        //            Email = contr.Email,
        //            RoleName = session != null ? session.RoleName : "",
        //            TokenHash = token,
        //            AuthorityForOneRoleModels = session != null ? session.AuthorityForOneRoleModels : null,
        //            CourseCash = session != null ? session.CourseCash : 0,
        //            CourseExtractCashless = session != null ? session.CourseExtractCashless : 0,
        //            CreditLimit = session != null ? session.CreditLimit : 0,
        //            DeliveryCity = contr.DeliveryCity,
        //            DeliveryHome = contr.DeliveryHome,
        //            DeliveryStreet = contr.DeliveryStreet,
        //            DeliveryAppartament = contr.DeliveryAppartament,
        //            DeliveryAdditional = contr.DeliveryAdditional,
        //            Phone = m.Phone,
        //            Contractor = session != null ? session.Contractor : null,
        //        };
        //        SessionHelpers.Session("user", session);
        //        return Json(new { Status = "Success" });
        //    }
        //    return Json(res != null ? new { Status = res.ExceptionMessage } : new { Status = "Error" });
        //}

        //public ActionResult VkCallback(string access_token = "", int expires_in = 0, int user_id = 0, string code = "", string error = "", string error_description = "")
        //{
        //    if (!String.IsNullOrEmpty(code))
        //    {
        //        var sessionExt = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
        //        if (sessionExt != null)
        //        {
        //            //System.Web.HttpContext.Current.Response.Write("<script>self.close();</script>");
        //            //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        //TODO check session if session == null send request else return main page
        //        //return
        //        //Redirect(
        //        //    "https://oauth.vk.com/access_token?client_id=5037819&client_secret=ugF5bwXI9uWFnAdf2tKr&redirect_uri=http://localhost:20169/Authorize/VkCallback&code=" +
        //        //    code);
        //        using (var client = new WebClient())
        //        {
        //            var responseString = client.DownloadString(new Uri(@"https://oauth.vk.com/access_token?client_id=5037819&client_secret=ugF5bwXI9uWFnAdf2tKr&redirect_uri=http://localhost:20169/Authorize/VkCallback&code=" + code));//.ConfigureAwait(false);
        //            var x = responseString;
        //            var fs = JsonConvert.DeserializeObject(responseString, typeof(ForVkModel)) as ForVkModel;
        //            if (fs != null)
        //            {
        //                var res = AuthFacade.ExternalSignIn(fs.user_id.ToString(), Provider.Vk, "", false).Result;
        //                if (res != null && res.ErrorCode == (int)ErrorCodes.Success)
        //                {
        //                    var session = new SessionModel
        //                    {
        //                        Name = res.NameContractor,
        //                        FatherName = res.FatherNameContractor,
        //                        SecondName = res.SecondNameContractor,
        //                        Email = res.Email,
        //                        RoleName = res.Role.Name,
        //                        TokenHash = res.TokenHash,
        //                        AuthorityForOneRoleModels = res.AuthorityForOneRoleModels,
        //                        CourseCash = res.CourseCash,
        //                        CourseExtractCashless = res.CourseExtractCashless,
        //                        CreditLimit = res.CreditLimit,
        //                        DeliveryCity = res.DeliveryCity,
        //                        DeliveryHome = res.DeliveryHome,
        //                        DeliveryStreet = res.DeliveryStreet,
        //                        DeliveryAppartament = res.DeliveryAppartament,
        //                        DeliveryAdditional = res.DeliveryAdditional,
        //                        Phone = res.Contractor != null ? res.Contractor.Phone : "",
        //                        Contractor = res.Contractor
        //                    };
        //                    SessionHelpers.Session("user", session);
        //                    SessionHelpers.Session("CountProducts", res.QuantityProducts);
        //                    if (res.Contractor == null) return RedirectToAction("Index", "Home");
        //                    var @from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //                    var journal = JournalOrderFacade.GetJournalOrdersWithDates(session.TokenHash, OrderType.AllOrders, @from, DateTime.Now, false).Result;
        //                    if (journal != null && journal.ErrorCode == (int)ErrorCodes.Success)
        //                    {
        //                        SessionHelpers.Session("journal", journal.OrderComesModels);
        //                    }
        //                }
        //                return RedirectToAction("Index", "Home");
        //            }
        //            //var fd = JsonConvert.DeserializeObject<ForVkModel>(responseString);


        //            //{"access_token":"4a2cf2a999dca23df51c4efaa79fb9accb5b85c2a3b8b507e610b72064bab30bdce98dad134be88397c3a","expires_in":86395,"user_id":17645055}
        //            //send request to our server (check in db and do same as in Login function)
        //        }
        //    }
        //    if (!string.IsNullOrEmpty(access_token) && user_id != 0)
        //    {
        //        return View();
        //    }
        //    return View();
        //}

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    //первичный запрос для внешних входов (соц.сети)
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Authorize", new { ReturnUrl = returnUrl }));
        //}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    //вход после подтверждения
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var provider = Provider.Microsoft;
        //    var email = "";
        //    if (loginInfo.Login.LoginProvider == "Facebook")
        //    {
        //        provider = Provider.Facebook;
        //    }
        //    if (loginInfo.Login.LoginProvider == "Google")
        //    {
        //        provider = Provider.Google;
        //        email = loginInfo.Email;
        //    }
        //    var res = AuthFacade.ExternalSignIn(loginInfo.Login.ProviderKey, provider, email, false).Result;
        //    if (res != null && res.ErrorCode == (int)ErrorCodes.Success)
        //    {
        //        var session = new SessionModel
        //        {
        //            Name = res.NameContractor,
        //            FatherName = res.FatherNameContractor,
        //            SecondName = res.SecondNameContractor,
        //            Email = res.Email,
        //            RoleName = res.Role.Name,
        //            TokenHash = res.TokenHash,
        //            AuthorityForOneRoleModels = res.AuthorityForOneRoleModels,
        //            CourseCash = res.CourseCash,
        //            CourseExtractCashless = res.CourseExtractCashless,
        //            CreditLimit = res.CreditLimit,
        //            DeliveryCity = res.DeliveryCity,
        //            DeliveryHome = res.DeliveryHome,
        //            DeliveryStreet = res.DeliveryStreet,
        //            DeliveryAppartament = res.DeliveryAppartament,
        //            DeliveryAdditional = res.DeliveryAdditional,
        //            Phone = res.Contractor != null ? res.Contractor.Phone : "",
        //            Contractor = res.Contractor
        //        };
        //        SessionHelpers.Session("user", session);
        //        SessionHelpers.Session("CountProducts", res.QuantityProducts);
        //        if (res.Contractor == null) return RedirectToAction("Index", "Home");
        //        var @from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        //        var journal = JournalOrderFacade.GetJournalOrdersWithDates(session.TokenHash, OrderType.AllOrders, @from, DateTime.Now, false).Result;
        //        if (journal != null && journal.ErrorCode == (int)ErrorCodes.Success)
        //        {
        //            SessionHelpers.Session("journal", journal.OrderComesModels);
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }
        //
        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }
        //
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //        private void SetViewBagCities()
        //        {
        //#if(CASH)
        //            var cities = Singl.GetOrderCities();
        //            if (cities != null)
        //                ViewBag.OrderCities = cities;
        //#else
        //            var resCities = CommonFacade.OrderCities().Result;
        //            if (resCities == null || resCities.ErrorCode != (int)ErrorCodes.Success) return;
        //            ViewBag.OrderCities = resCities.OrderCities;
        //#endif
        //        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        //internal class ChallengeResult : HttpUnauthorizedResult
        //{
        //    public ChallengeResult(string provider, string redirectUri)
        //        : this(provider, redirectUri, null)
        //    {
        //    }

        //    public ChallengeResult(string provider, string redirectUri, string userId)
        //    {
        //        LoginProvider = provider;
        //        RedirectUri = redirectUri;
        //        UserId = userId;
        //    }

        //    public string LoginProvider { get; set; }
        //    public string RedirectUri { get; set; }
        //    public string UserId { get; set; }

        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
        //        if (UserId != null)
        //        {
        //            properties.Dictionary[XsrfKey] = UserId;
        //        }
        //        context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
        //    }
        //}
        #endregion
    }
}