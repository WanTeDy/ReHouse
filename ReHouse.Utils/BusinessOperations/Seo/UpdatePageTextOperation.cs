using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Common;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Seo
{
    public class UpdatePageTextOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public PageText _pageText { get; set; }

        public UpdatePageTextOperation(string tokenHash, PageText pageText)
        {
            _tokenHash = tokenHash;
            _pageText = pageText;
            RussianName = "Изменение сео текста";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            PageText pageText = Context.PageTexts.FirstOrDefault(x => x.Id == _pageText.Id && !x.Deleted);
            if (pageText == null)
            {
                Errors.Add("Id", "*Страница не найдена!");
            }
            else
            {
                pageText.Title = _pageText.Title;
                pageText.Description = _pageText.Description;
                Context.SaveChanges();
            }
        }
    }
}