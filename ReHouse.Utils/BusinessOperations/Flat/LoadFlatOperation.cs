using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Flat //TODO
{
    public class LoadFlatOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _id { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        public Advert _advert { get; set; }
        public List<Advert> _adverts { get; set; }
        public Dictionary<AdvertProperty, AdvertPropertyValue> _properties { get; set; }
        public Int32 _square { get; set; }

        public LoadFlatOperation(string tokenHash, int id, int page, int count)
        {
            _tokenHash = tokenHash;
            _id = id;
            _page = page;
            _count = count;
            RussianName = "Получение обьявления";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _advert = Context.Adverts.FirstOrDefault(x => !x.Deleted && x.Id == _id);
            if (_advert != null)
            {
                if (_page != 0)
                    _adverts = Context.Adverts.Where(x => !x.Deleted && x.Id != _id && x.Type == _advert.Type && x.Category.ParentId == _advert.Category.ParentId).OrderByDescending(x => x.IsHot)
                        .ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();

                var advertProperties = Context.AdvertProperties.Where(x => !x.Deleted && x.Categories
                    .Any(y => y.Id == _advert.Category.ParentId)).OrderBy(x => x.Priority).ToList();
                var advertValues = _advert.AdvertPropertyValues.ToList();

                _properties = new Dictionary<AdvertProperty, AdvertPropertyValue>();
                foreach (var prop in advertProperties)
                {
                    _properties.Add(prop, advertValues.FirstOrDefault(x => x.AdvertPropertyId == prop.Id));
                }
                int temp = 0;
                var propertySquare = _properties.FirstOrDefault(x => x.Key.Priority == 1).Value;
                if (propertySquare != null)
                    Int32.TryParse(propertySquare.PropertiesValue, out temp);
                _square = temp;
            }
        }
    }
}