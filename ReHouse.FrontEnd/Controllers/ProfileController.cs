using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ReHouse.Utils.BusinessOperations.Auth.Roles;
using ReHouse.Utils.Except;
using ReHouse.FrontEnd.Helpers;
using ReHouse.FrontEnd.Models;

namespace ReHouse.FrontEnd.Controllers
{
    public class ProfileController : Controller
    {
        //private String _token { get; set; }
        //private SessionModel _userSession { get; set; }
        //public ProfileController()
        //{
        //    _token = "";
        //    _userSession = SessionHelpers.Session("user", typeof(SessionModel)) as SessionModel;
        //    if (_userSession != null)
        //        _token = _userSession.TokenHash;
        //    if (String.IsNullOrEmpty(_token))
        //        throw new ActionNotAllowedException("Получено пустое значение Token, пожалуйста перезайдите в свою учетную запись!");
        //}

        //// GET: Profile
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult Roles()
        //{            
        //    var operation = new LoadDataRolesOperation(_token);
        //    operation.ExcecuteTransaction();
        //    return View(operation._roles);
        //}
    }
}