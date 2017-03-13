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
    public class EmailController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return PartialView("Partial/_emailPartial", model);
        }        
    }
}