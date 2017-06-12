using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class UpdateSeoParamOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public SeoParam _seoParam { get; set; }

        public UpdateSeoParamOperation(string tokenHash, SeoParam seoParam)
        {
            _tokenHash = tokenHash;
            _seoParam = seoParam;
            RussianName = "Изменение сео параметров";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            SeoParam seoParam = Context.SeoParams.FirstOrDefault(x => x.Id == _seoParam.Id && !x.Deleted);
            if (seoParam == null)
            {
                _seoParam.ActionName = _seoParam.ActionName.ToLower();
                _seoParam.ControllerName = _seoParam.ControllerName.ToLower();
                _seoParam.FullUrl = _seoParam.FullUrl.ToLower();
                Context.SeoParams.Add(_seoParam);
            }
            else
            {
                seoParam.Title = _seoParam.Title;
                seoParam.Description = _seoParam.Description;
                seoParam.Keywords = _seoParam.Keywords;
            }
            Context.SaveChanges();
        }
    }
}