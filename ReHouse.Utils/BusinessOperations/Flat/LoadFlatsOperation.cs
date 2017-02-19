﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class LoadFlatsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        private Int32 _districtId { get; set; }
        private Int32 _priceId { get; set; }
        private Int32 _trimConditionId { get; set; }
        private Int32 _categoryId { get; set; }
        private Boolean _isOnlyHot { get; set; }
        private AdvertsType _advertsType { get; set; }
        public Category _category { get; set; }
        public List<Advert> _adverts { get; set; }

        public LoadFlatsOperation(string tokenHash, int page, int count, int districtId, int priceId, 
            int trimConditionId, int categoryId, AdvertsType advertsType, bool IsOnlyHot)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            _districtId = districtId;
            _priceId = priceId;
            _trimConditionId = trimConditionId;
            _categoryId = categoryId;
            _advertsType = advertsType;
            _isOnlyHot = IsOnlyHot;
            RussianName = "Получение нужного кол-ва объявлений c нужным фильтром";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _adverts = Context.Adverts.Where(x => !x.Deleted).ToList();
            if(_advertsType != AdvertsType.All)
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
            if (_priceId != 0)
            {
                var priceFilter = Context.PriceFilters.FirstOrDefault(x => !x.Deleted && x.Id == _priceId);
                if (priceFilter != null)
                {
                    _adverts = _adverts.Where(x => x.Price >= priceFilter.Min && x.Price < priceFilter.Max).ToList();
                }
            }
            if (_trimConditionId != 0)
            {
                _adverts = _adverts.Where(x => !x.Deleted && x.TrimConditionId == _trimConditionId).ToList();
            }
            if (_categoryId != 0)
            {
                _category = Context.Categories.FirstOrDefault(x => !x.Deleted && x.Id == _categoryId);
                if(_category != null && _category.Parent != null)
                    _adverts = _adverts.Where(x => !x.Deleted && x.CategoryId == _categoryId).ToList();
                else
                    _adverts = _adverts.Where(x => !x.Deleted && x.Category.ParentId == _categoryId).ToList();
            }
            _adverts = _adverts.OrderByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}