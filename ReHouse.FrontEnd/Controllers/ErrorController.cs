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
    public class ErrorController : BaseController
    {
        public ActionResult NotFound()
        {
            ViewBag.NotShowNews = true;
            return View();
        }
        public ActionResult InternalServerError()
        {
            ViewBag.NotShowNews = true;
            return View();
        }
    }
}