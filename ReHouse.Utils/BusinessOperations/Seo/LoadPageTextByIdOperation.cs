using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class LoadPageTextByIdOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        public PageText _text { get; set; }

        public LoadPageTextByIdOperation(string tokenHash, int id)
        {
            _tokenHash = tokenHash;
            _id = id;
            RussianName = "Получение текста для страниц";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _text = Context.PageTexts.FirstOrDefault(x => !x.Deleted && x.Id == _id);
        }
    }
}