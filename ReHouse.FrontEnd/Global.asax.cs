using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ReHouse.Utils.BusinessOperations.AuthoritiesOp;
using ReHouse.FrontEnd.Helpers;

namespace ReHouse.FrontEnd
{
    public class MvcApplication : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            var operation = new FillAuthorityOperation();
            operation.ExcecuteTransaction();
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception objErr = Server.GetLastError().GetBaseException();
        //    if (objErr is HttpException)
        //    {
        //        var httpEx = objErr as HttpException;
        //        if (httpEx.GetHttpCode() == 404)
        //        {
        //            Response.Redirect("/Error/NotFound");
        //        }
        //        else
        //        {
        //            string err = "Critical ERROR: " +
        //            "Error for: " + Request.Url.ToString() +
        //            "\nError message: " + objErr.Message.ToString() +
        //            "\nStackTrace: " + objErr.StackTrace.ToString().Substring(0, 100) +
        //            "\n";
        //            logger.Error(err);
        //            Server.ClearError();
        //            Response.Redirect("/Error/InternalServerError");
        //        }
        //    }
        //    else
        //    {
        //        string err = " HERE IS ERROR: " +
        //            "Error for: " + Request.Url.ToString() +
        //            "\nError message: " + objErr.Message.ToString() +
        //            "\nStackTrace: " + objErr.StackTrace.ToString().Substring(0, 100) +
        //            "\n";
        //        logger.Error(err);
        //        Server.ClearError();
        //        Response.Redirect("/Error/InternalServerError");
        //    }
        //}  
    }
}