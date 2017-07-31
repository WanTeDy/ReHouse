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
using ReHouse.Utils.DataBase.Feedback;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.BusinessOperations.Emails;
using ReHouse.FrontEnd.Filters;

namespace ReHouse.FrontEnd.Controllers
{
    public class EmailController : BaseController
    {
        [CanonicalAttribute("email/")]
        public ActionResult Index(int flat = 0, AdvertsType type = 0)
        {
            ViewBag.AdvertId = flat;       
            ViewBag.Type = type;
            ViewBag.NotShowNews = true;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(EmailModel model, int advertId = 0, AdvertsType type = 0)
        {
            ViewBag.AdvertId = advertId;
            ViewBag.Type = type;
            if (ModelState.IsValid)
            {
                var operation = new SendEmailOperation(new UserEmailMessage
                {
                    Message = model.Message,
                    Phone = model.Phone,
                    Username = model.FirstName
                }, advertId, type);
                operation.ExcecuteTransaction();
                ViewBag.Success = true;
            }
            return PartialView("Partial/_emailPartial", model);
        }        
    }
}