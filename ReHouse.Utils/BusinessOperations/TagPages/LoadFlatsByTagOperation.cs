using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.TagPages
{
    public class LoadFlatsByTagOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        private String _tagPageName { get; set; }
        public TagPage _tagPage { get; set; }
        public List<Advert> _adverts { get; set; }

        public LoadFlatsByTagOperation(string tokenHash, int page, int count, string tagPageName)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            _tagPageName = tagPageName;
            RussianName = "Получение нужного кол-ва объявлений c по тегу";
        }

        protected override void InTransaction()
        {
            _tagPage = Context.TagPages.FirstOrDefault(x => !x.Deleted && x.ShortName.ToLower() == _tagPageName.Trim().ToLower());
            if (_tagPage == null)
                return;

            switch (_tagPage.TagPageType)
            {
                case TagPageType.Exclusive:
                    _adverts = Context.Adverts.Where(x => !x.Deleted && x.IsModerated && x.IsExclusive && x.Type == _tagPage.AdvertsType).ToList();
                    break;
                case TagPageType.Category:
                    var category = Context.Categories.FirstOrDefault(x => x.TagPages.Any(y => y.Id == _tagPage.Id));
                    if (category == null)
                        return;
                    _adverts = Context.Adverts.Where(x => !x.Deleted && x.IsModerated && x.CategoryId == category.Id && x.Type == _tagPage.AdvertsType).ToList();
                    break;
                case TagPageType.District:
                    var district = Context.Districts.FirstOrDefault(x => x.TagPages.Any(y => y.Id == _tagPage.Id));
                    if (district == null)
                        return;
                    _adverts = Context.Adverts.Where(x => !x.Deleted && x.IsModerated && x.DistrictId == district.Id && x.Type == _tagPage.AdvertsType).ToList();
                    break;
            }

            _adverts = _adverts.OrderByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();

            _adverts.ForEach(
                x =>
                {
                    if (!String.IsNullOrEmpty(x.Description) && x.Description.Length > ConstV.DescMinimizeSymbols + 5)
                        x.Description = x.Description.Substring(0, ConstV.DescMinimizeSymbols) + "...";
                });
        }
    }
}