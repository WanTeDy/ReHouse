using System.Web.Mvc;

namespace ReHouse.FrontEnd.Areas.Cabinet
{
    public class CabinetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cabinet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cabinet_default",
                "Cabinet/{controller}/{action}/{id}",
                new { controller = "profile", action = "index", id = UrlParameter.Optional },
                new[] { "ReHouse.FrontEnd.Areas.Cabinet.Controllers" }

            );
        }
    }
}