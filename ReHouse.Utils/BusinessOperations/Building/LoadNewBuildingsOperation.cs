using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.Except;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class LoadNewBuildingsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        private Int32 _districtId { get; set; }
        private Int32 _priceMin { get; set; }
        private Int32 _priceMax { get; set; }
        private Int32 _builderId { get; set; }
        private Int32 _expluatationDate { get; set; }
        private Int32 _userId { get; set; }
        private Boolean _isAdmin { get; set; }
        public List<NewBuilding> _newBuildings { get; set; }

        public LoadNewBuildingsOperation(string tokenHash, int page, int count, int districtId, int priceMin, int priceMax, int builderId, int expluatationDate, int userId, bool isAdmin = false)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            _districtId = districtId;
            _priceMin = priceMin;
            _priceMax = priceMax;
            _builderId = builderId;
            _expluatationDate = expluatationDate;
            _userId = userId;
            _isAdmin = isAdmin;
            RussianName = "Получение нужного кол-ва новостроев объявлений c нужным фильтром";
        }

        protected override void InTransaction()
        {
            if (_isAdmin)
            {
                new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
                var user = Context.Users.FirstOrDefault(x => x.TokenHash == _tokenHash);
                if (user != null && (user.Role.RussianName == ConstV.RoleAdministrator || user.Role.RussianName == ConstV.RoleManager || user.Role.RussianName == ConstV.RoleSeo))
                {
                    _newBuildings = Context.NewBuildings.Where(x => !x.Deleted).ToList();
                    //if (_userId != 0)
                    //{
                    //    _newBuildings = _newBuildings.Where(x => x.UserId == _userId).ToList();
                    //}
                }
                //else if (user != null && user.Role.RussianName == ConstV.RoleNewBuildingRieltor)
                //{
                //    _newBuildings = Context.NewBuildings.Where(x => !x.Deleted && x.UserId == user.Id).ToList();
                //}
                else
                    throw new ActionNotAllowedException("Недостаточно прав доступа на выполнение операции");
            }
            else
                _newBuildings = Context.NewBuildings.Where(x => !x.Deleted && x.IsModerated).ToList();

            if (_districtId != 0)
            {
                _newBuildings = _newBuildings.Where(x => x.DistrictId == _districtId).ToList();
            }
            if (_priceMin != 0 || _priceMax != 0)
            {
                //var priceFilter = Context.PriceFilters.FirstOrDefault(x => !x.Deleted && x.Id == _priceMin && x.AdvertType == Helpers.AdvertsType.NewBuilding);
                //if (priceFilter != null)
                //{
                //    _newBuildings = _newBuildings.Where(x => x.Price >= priceFilter.Min && x.Price < priceFilter.Max).ToList();
                //}
                _newBuildings = _newBuildings.Where(x => x.Price >= _priceMin && x.Price <= _priceMax).ToList();
            }
            if (_builderId != 0)
            {
                _newBuildings = _newBuildings.Where(x => x.Builders.FirstOrDefault(s => s.Id == _builderId) != null).ToList();
            }
            if (_expluatationDate != 0)
            {
                _newBuildings = _newBuildings.Where(x => x.ExpluatationDate.Year <= _expluatationDate).ToList();
            }
            if (_isAdmin)
                _newBuildings = _newBuildings.OrderBy(x => x.IsModerated).ThenByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();
            else if (_expluatationDate != 0)
                _newBuildings = _newBuildings.OrderByDescending(x => x.ExpluatationDate.Year).ThenByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();
            else
                _newBuildings = _newBuildings.OrderByDescending(x => x.IsHot).ThenByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();

        }
    }
}