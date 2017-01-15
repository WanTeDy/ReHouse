using System;
using System.Text;
using System.Web.Mvc;
using ReHouse.FrontEnd.Models;

namespace ReHouse.FrontEnd.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
                                              PagingInfo pagingInfo,
                                              Func<int, string> pageUrl)
        {
            var result = new StringBuilder();

            const int first = 1;
            var middle1 = true;
            var previousPage = pagingInfo.CurrentPage - 1;
            var nextPage = pagingInfo.CurrentPage + 1;
            var middle2 = true;
            var lastPage = pagingInfo.TotalPages;

            if (first > previousPage)
                previousPage = first;
            if (nextPage > lastPage)
                nextPage = lastPage;

            if (first >= previousPage - 1)
                middle1 = false;
            if (nextPage >= lastPage - 1)
                middle2 = false;

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                if (i == first || i == previousPage || i == pagingInfo.CurrentPage || i == nextPage || i == lastPage)
                {
                    TagBuilder tag = new TagBuilder("li");
                    if (i == pagingInfo.CurrentPage)
                    {
                        tag.AddCssClass("active");
                    }

                    var tag2 = new TagBuilder("a");
                    tag2.AddCssClass("pager-link");
                    tag2.MergeAttribute("href", pageUrl(i));
                    tag2.InnerHtml = i.ToString();
                    tag.InnerHtml = tag2.ToString();
                    result.Append(tag.ToString());
                    if ((first == i && middle1) || (nextPage == i && middle2))
                    {
                        tag = new TagBuilder("li");
                        tag.AddCssClass("m");
                        tag2 = new TagBuilder("span");
                        tag2.AddCssClass("middle");
                        tag2.InnerHtml = "...";
                        tag.InnerHtml = tag2.ToString();
                        result.Append(tag.ToString());
                    }
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}