﻿using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.Security;

namespace ReHouse.Utils.BusinessOperations.Filters
{
    public class LoadFiltersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _categoryId { get; set; }
        private Boolean _isAdmin { get; set; }
        private AdvertsType _advertsType { get; set; }
        public List<Category> _categories { get; set; }
        public List<Builder> _builders { get; set; }
        public List<PriceFilter> _prices { get; set; }
        public List<District> _districts { get; set; }
        public List<ExpluatationDate> _expluatationDates { get; set; }
        public List<TrimCondition> _trimConditions { get; set; }
        public List<User> _users { get; set; }

        public LoadFiltersOperation(string tokenHash, AdvertsType advertsType, int categoryId = 0, bool isAdmin = false)
        {
            _tokenHash = tokenHash;
            _categoryId = categoryId;
            _advertsType = advertsType;
            _isAdmin = isAdmin;
            RussianName = "Получение параметров фильтров объявлений";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
            if (user != null && (user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleManager))
            {
                _users = Context.Users.Where(x => !x.Deleted).ToList();
            }
            if (_advertsType == Helpers.AdvertsType.NewBuilding)
            {
                _builders = Context.Builders.Where(x => !x.Deleted).ToList();
                if(_isAdmin)
                    _expluatationDates = Context.ExpluatationDates.Where(x => !x.Deleted).ToList();
                else
                    _expluatationDates = Context.ExpluatationDates.Where(x => !x.Deleted && x.NewBuildings.Any(y => !y.Deleted)).ToList();
            }
            else
            {
                if (_categoryId == 0)
                {
                    _categories = Context.Categories.Where(x => !x.Deleted && x.ParentId == null).ToList();
                }
                else
                {
                    _categories = Context.Categories.Where(x => !x.Deleted && x.ParentId == _categoryId).ToList();
                }
                _trimConditions = Context.TrimConditions.Where(x => !x.Deleted).ToList();
            }
            _districts = Context.Districts.Where(x => !x.Deleted).OrderBy(x => x.RussianName).ToList();
            _prices = Context.PriceFilters.Where(x => !x.Deleted && x.AdvertType == _advertsType).ToList();
        }
    }
}