using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class LoadFlatsOperation : BaseOperation
    {
        private Int32 _length = 90;
        private Int32 _subLength = 85;
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        private Int32 _districtId { get; set; }
        private Int32 _priceMin { get; set; }
        private Int32 _priceMax { get; set; }
        private Int32 _trimConditionId { get; set; }
        private Int32 _userId { get; set; }
        private Int32 _categoryId { get; set; }
        private Boolean _isOnlyHot { get; set; }
        //private Boolean _isOnlyUser { get; set; }
        private Boolean _isAdmin { get; set; }
        private AdvertsType _advertsType { get; set; }
        public Category _category { get; set; }
        public List<Advert> _adverts { get; set; }

        public LoadFlatsOperation(string tokenHash, int page, int count, int districtId, int priceMin, int priceMax,
            int trimConditionId, int userId, int categoryId, AdvertsType advertsType, bool IsOnlyHot, /*bool IsOnlyUser = false,*/ bool IsAdmin = false)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            _districtId = districtId;
            _priceMin = priceMin;
            _priceMax = priceMax;
            _trimConditionId = trimConditionId;
            _categoryId = categoryId;
            _advertsType = advertsType;
            _isOnlyHot = IsOnlyHot;
            _userId = userId;
            //_isOnlyUser = IsOnlyUser;
            _isAdmin = IsAdmin;
            RussianName = "Получение нужного кол-ва объявлений c нужным фильтром";
        }

        protected override void InTransaction()
        {
            if (_isAdmin)
            {
                new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
                var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
                if (user != null && (user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleManager || user.Role.RussianName == ConstV.RoleSeo))
                {
                    _adverts = Context.Adverts.Where(x => !x.Deleted).ToList();
                    if (_userId != 0)
                    {
                        _adverts = _adverts.Where(x => x.UserId == _userId).ToList();
                    }
                }
                else if (user != null && user.Role.RussianName == ConstV.RoleRieltor)
                {
                    _adverts = Context.Adverts.Where(x => !x.Deleted && x.UserId == user.Id).ToList();
                }
                else
                    throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции");
            }
            else
                _adverts = Context.Adverts.Where(x => !x.Deleted && x.IsModerated).ToList();
            if (_advertsType != AdvertsType.All)
            {
                _adverts = _adverts.Where(x => x.Type == _advertsType).ToList();
            }
            if (_isOnlyHot)
            {
                _adverts = _adverts.Where(x => x.IsHot).ToList();
            }
            if (_districtId != 0)
            {
                _adverts = _adverts.Where(x => x.DistrictId == _districtId).ToList();
            }
            if (_priceMin != 0 || _priceMax != 0)
            {
                //var priceFilter = Context.PriceFilters.FirstOrDefault(x => !x.Deleted && x.Id == _priceMin);
                //if (priceFilter != null)
                //{
                //    _adverts = _adverts.Where(x => x.Price >= priceFilter.Min && x.Price < priceFilter.Max).ToList();
                //}
                _adverts = _adverts.Where(x => x.Price >= _priceMin && x.Price <= _priceMax).ToList();
            }
            if (_trimConditionId != 0)
            {
                _adverts = _adverts.Where(x => !x.Deleted && x.TrimConditionId == _trimConditionId).ToList();
            }
            if (_categoryId != 0)
            {
                _category = Context.Categories.FirstOrDefault(x => !x.Deleted && x.Id == _categoryId);
                if (_category != null && _category.Parent != null)
                    _adverts = _adverts.Where(x => !x.Deleted && x.CategoryId == _categoryId).ToList();
                else
                    _adverts = _adverts.Where(x => !x.Deleted && x.Category.ParentId == _categoryId).ToList();
            }
            if(_isAdmin)
                _adverts = _adverts.OrderBy(x => x.IsModerated).ThenByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();
            else
                _adverts = _adverts.OrderByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();

            _adverts.ForEach(
                x =>
                {
                    if (!String.IsNullOrEmpty(x.Description) && x.Description.Length > _length)
                        x.Description = x.Description.Substring(0, _subLength) + "...";
                });
        }
    }
}