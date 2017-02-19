using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class LoadNewBuildingsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _page { get; set; }
        private Int32 _count { get; set; }
        private Int32 _districtId { get; set; }
        private Int32 _priceId { get; set; }
        private Int32 _builderId { get; set; }
        private Int32 _expluatationDateId { get; set; }
        public List<NewBuilding> _newBuildings { get; set; }

        public LoadNewBuildingsOperation(string tokenHash, int page, int count, int districtId, int priceId, int builderId, int expluatationDateId)
        {
            _tokenHash = tokenHash;
            _page = page;
            _count = count;
            _districtId = districtId;
            _priceId = priceId;
            _builderId = builderId;
            _expluatationDateId = expluatationDateId;
            RussianName = "Получение нужного кол-ва новостроев объявлений c нужным фильтром";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            List<NewBuilding> tempList = Context.NewBuildings.Where(x => !x.Deleted).ToList();
            if (_districtId != 0)
            {
                tempList = tempList.Where(x => x.DistrictId == _districtId).ToList();
            }
            if (_priceId != 0)
            {
                var priceFilter = Context.PriceFilters.FirstOrDefault(x => !x.Deleted && x.Id == _priceId && x.AdvertType == Helpers.AdvertsType.NewBuilding);
                if (priceFilter != null)
                {
                    tempList = tempList.Where(x => x.Price >= priceFilter.Min && x.Price < priceFilter.Max).ToList();
                }
            }
            if (_builderId != 0)
            {
                tempList = tempList.Where(x => x.Builders.FirstOrDefault(s => s.Id == _builderId) != null).ToList();
            }
            if (_expluatationDateId != 0)
            {
                tempList = tempList.Where(x => x.ExpluatationDateId == _expluatationDateId).ToList();
            }
            _newBuildings = tempList.OrderByDescending(x => x.PublicationDate).Skip((_page - 1) * _count).Take(_count).ToList();
        }
    }
}