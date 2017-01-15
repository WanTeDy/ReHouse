using System.Text;
using System.Web.Mvc;

namespace ReHouse.FrontEnd.Helpers
{
    public static class TextOutHelpers
    {
        public static MvcHtmlString HtmlText(this HtmlHelper html,
            string text)
        {
            var result = new StringBuilder();
            text = text.Replace("&lt;", " <").Replace("&gt;", ">");
            return MvcHtmlString.Create(text);
        }
    }
}