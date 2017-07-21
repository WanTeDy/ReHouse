using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.TagPages
{
    public class LoadTagPageByIdOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        public TagPage _tagPage { get; set; }

        public LoadTagPageByIdOperation(string tokenHash, int id)
        {
            _tokenHash = tokenHash;
            _id = id;
            RussianName = "Получение страницы тега";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _tagPage = Context.TagPages.FirstOrDefault(x => !x.Deleted && x.Id == _id);
        }
    }
}