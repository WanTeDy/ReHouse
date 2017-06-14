using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class LoadSeoParamOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _action { get; set; }
        private String _controller { get; set; }
        private String _url { get; set; }
        private String _urlParams { get; set; }
        public SeoParam _seoParams { get; set; }

        public LoadSeoParamOperation(string tokenHash, string action, string controller, string url, string urlParams)
        {
            _tokenHash = tokenHash;
            _action = action.ToLower();
            _controller = controller.ToLower();
            _url = url.ToLower();
            _urlParams = urlParams;
            RussianName = "Получение сео параметров для страниц";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            if (!String.IsNullOrEmpty(_urlParams))
            {
                _seoParams = Context.SeoParams.FirstOrDefault(x => !x.Deleted && x.ActionName == _action && x.ControllerName == _controller && x.FullUrl == _url);
            }
            else
            {
                _seoParams = Context.SeoParams.FirstOrDefault(x => !x.Deleted && x.ActionName == _action && x.ControllerName == _controller);
                if (_seoParams == null)
                    Context.SeoParams.Add(new SeoParam
                    {
                        ActionName = _action,
                        ControllerName = _controller,
                        FullUrl = "/" + _controller + "/" + _action + "/"
                    });
                Context.SaveChanges();
            }
        }
    }
}