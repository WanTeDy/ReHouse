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
        public SeoParam SeoParam { get; set; }

        public UpdateSeoParamOperation(string tokenHash, SeoParam seoParam)
        {
            _tokenHash = tokenHash;
            SeoParam = seoParam;
            RussianName = "Изменение сео параметров";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            SeoParam seoParam = Context.SeoParams.FirstOrDefault(x => x.Id == SeoParam.Id && !x.Deleted);
            if (seoParam == null)
            {
                Errors.Add("Id", "*Страница не найдена!");
            }
            else
            {
                seoParam.Title = SeoParam.Title;
                seoParam.Description = SeoParam.Description;
                seoParam.Keywords = SeoParam.Keywords;
                Context.SaveChanges();
            }
        }
    }
}