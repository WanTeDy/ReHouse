using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.TagPages
{
    public class UpdateTagPageOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public TagPage _tagPage { get; set; }

        public UpdateTagPageOperation(string tokenHash, TagPage tagPage)
        {
            _tokenHash = tokenHash;
            _tagPage = tagPage;
            RussianName = "Изменение страниц тегов";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            TagPage tagPage = Context.TagPages.FirstOrDefault(x => x.Id == _tagPage.Id && !x.Deleted);
            if (tagPage == null)
            {
                Errors.Add("Id", "*Страница не найдена!");
            }
            else
            {
                tagPage.RussianName = _tagPage.RussianName;
                tagPage.SeoText = _tagPage.SeoText;
                tagPage.ShortName = _tagPage.ShortName;
                tagPage.IsActive = _tagPage.IsActive;
                Context.SaveChanges();
            }
        }
    }
}