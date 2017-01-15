using System.Web;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Helpers
{
    public static class UrlHelpers
    {
        public static MvcHtmlString UrlRequest(this HtmlHelper html)
        {
            string user_agent = HttpContext.Current.Request.UserAgent;
            string url = HttpContext.Current.Request.RawUrl;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string referrer = HttpContext.Current.Request.UrlReferrer == null ? "" : HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
            var res = "<p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";

            return MvcHtmlString.Create(res);
        }
    }
}