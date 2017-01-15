using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReHouse.FrontEnd.Models;


namespace ReHouse.FrontEnd.Filters
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] _allowedUsers = new string[] { };
        private string[] _allowedRoles = new string[] { };
        public SessionModel SessionModel { get; set; }
        public MyAuthorizeAttribute()
        { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(base.Users))
            {
                _allowedUsers = base.Users.Split(new char[] { ',' });
                for (int i = 0; i < _allowedUsers.Length; i++)
                {
                    _allowedUsers[i] = _allowedUsers[i].Trim();
                }
            }
            if (!String.IsNullOrEmpty(base.Roles))
            {
                _allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < _allowedRoles.Length; i++)
                {
                    _allowedRoles[i] = _allowedRoles[i].Trim();
                }
            }
            SessionModel = GetSession(httpContext);
            if (SessionModel == null)
                return false;
            if (User(SessionModel) && Role(SessionModel) && httpContext.Request.IsAuthenticated) return true;
            DeleteCookies(httpContext);
            return false;

            //try
            //{
            //    var authResponse = AuthFacade.CheckSignInData().Result;//CheckWorkerSignInByHash(UserLoginModel.TokenHash).Result;
            //    if (authResponse.Worker == null)
            //    {
            //        DeleteCookies(httpContext);
            //        return false;
            //    }
            //    httpContext.User = new MyUser(authResponse.Worker.Role.Name, UserLoginModel.Name, UserLoginModel.TokenHash, true);
            //    return User(httpContext) && Role(httpContext);
            //}
            //catch (Exception)
            //{
            //    return false;
            //}

        }

        private static void DeleteCookies(HttpContextBase httpContext)
        {
            if (httpContext.Session == null || httpContext.Session["user"] == null) return;
            httpContext.Session["user"] = null;
        }
        private static SessionModel GetSession(HttpContextBase httpContext)
        {
            if (httpContext.Session == null) return null;
            var session = httpContext.Session["user"] as SessionModel;
            return session;
            //httpContext.Session.Timeout = 
        }

        private bool User(SessionModel sessionModel)
        {
            //var user = GetSession(httpContext);
            if (_allowedUsers.Length > 0 && sessionModel != null)
            {
                return _allowedUsers.Contains(sessionModel.Name);
            }
            return true;
        }

        private bool Role(SessionModel sessionModel)
        {
            if (_allowedRoles.Length > 0 && sessionModel != null)
            {
                return _allowedRoles.Contains(sessionModel.Name);
            }
            return true;
        }
    }
}