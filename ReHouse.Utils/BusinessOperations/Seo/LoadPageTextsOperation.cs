using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class LoadPageTextsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private String _action { get; set; }
        private String _controller { get; set; }
        public List<PageText> _pageTexts { get; set; }

        public LoadPageTextsOperation(string tokenHash, string action, string controller)
        {
            _tokenHash = tokenHash;
            _action = action;
            _controller = controller;
            RussianName = "Получение сео текстов для страниц";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            //_pageTexts = Context.PageTexts.Where(x => !x.Deleted && x.ActionName == _action && x.ControllerName == _controller).ToList();
        }
    }
}