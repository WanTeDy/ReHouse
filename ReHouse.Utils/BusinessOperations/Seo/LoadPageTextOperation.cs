using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class LoadPageTextOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _action { get; set; }
        private String _controller { get; set; }
        private String _url { get; set; }
        private String _urlParams { get; set; }
        public List<PageText> _pageTexts { get; set; }

        public LoadPageTextOperation(string tokenHash, string action, string controller, string url, string urlParams)
        {
            _tokenHash = tokenHash;
            _action = action.ToLower();
            _controller = controller.ToLower();
            _url = url.ToLower();
            _urlParams = urlParams;
            RussianName = "Получение текстов для страниц";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            if (!String.IsNullOrEmpty(_urlParams))
            {
                _pageTexts = Context.PageTexts.Where(x => !x.Deleted && x.ActionName == _action && x.ControllerName == _controller && x.FullUrl == _url).ToList();
                if (_pageTexts.Count == 0)
                    _pageTexts = Context.PageTexts.Where(x => !x.Deleted && x.ActionName == _action && x.ControllerName == _controller).ToList();
            }
            else
            {
                _pageTexts = Context.PageTexts.Where(x => !x.Deleted && x.ActionName == _action && x.ControllerName == _controller).ToList();
                if (_pageTexts.Count == 0)
                    Context.PageTexts.Add(new PageText
                    {
                        ActionName = _action,
                        ControllerName = _controller,
                        FullUrl = "/" + _controller + "/" + _action + "/",
                        TextBlockName = "Main",
                        Title = "Заголовок",
                        Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.",
                    });
                Context.SaveChanges();
            }
        }
    }
}