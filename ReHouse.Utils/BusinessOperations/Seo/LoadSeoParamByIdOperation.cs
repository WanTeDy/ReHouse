using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class LoadSeoParamByIdOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        public SeoParam _seoParams { get; set; }

        public LoadSeoParamByIdOperation(string tokenHash, int id)
        {
            _tokenHash = tokenHash;
            _id = id;
            RussianName = "Получение сео параметров для страниц";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _seoParams = Context.SeoParams.FirstOrDefault(x => !x.Deleted && x.Id == _id);
        }
    }
}