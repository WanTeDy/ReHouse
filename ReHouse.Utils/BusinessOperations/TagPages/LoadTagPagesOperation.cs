using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.TagPages
{
    public class LoadTagPagesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private AdvertsType _type { get; set; }
        public List<TagPage> _tagPages { get; set; }

        public LoadTagPagesOperation(string tokenHash, AdvertsType type)
        {
            _tokenHash = tokenHash;
            _type = type;
            RussianName = "Получение список тегов";
        }

        protected override void InTransaction()
        {
            _tagPages = Context.TagPages.Where(x => !x.Deleted && x.AdvertsType == _type).ToList();
            if(_tagPages != null && _tagPages.Count > 0)
            {
                foreach(var page in _tagPages)
                {
                    switch (page.TagPageType)
                    {
                        case TagPageType.Exclusive:
                            page.Quantity = Context.Adverts.Count(x => !x.Deleted && x.IsModerated && x.IsExclusive && x.Type == page.AdvertsType);
                            break;
                        case TagPageType.Category:
                            var category = Context.Categories.FirstOrDefault(x => x.TagPages.Any(y => y.Id == page.Id));
                            if (category == null)
                                return;
                            page.Quantity = Context.Adverts.Count(x => !x.Deleted && x.IsModerated && x.CategoryId == category.Id && x.Type == page.AdvertsType);
                            break;
                        case TagPageType.District:
                            var district = Context.Districts.FirstOrDefault(x => x.TagPages.Any(y => y.Id == page.Id));
                            if (district == null)
                                return;
                            page.Quantity = Context.Adverts.Count(x => !x.Deleted && x.IsModerated && x.DistrictId == district.Id && x.Type == page.AdvertsType);
                            break;
                    }
                }
            }            
        }
    }
}