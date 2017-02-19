using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.AdvertParams;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class LoadNewBuildingFiltersOperation : BaseOperation
    {
        private String _tokenHash { get; set; }        
        public List<Builder> _builders { get; set; }
        public List<PriceFilter> _prices { get; set; }
        public List<District> _districts { get; set; }
        public List<ExpluatationDate> _expluatationDates { get; set; }

        public LoadNewBuildingFiltersOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение параметров фильтров";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _builders = Context.Builders.Where(x => !x.Deleted).ToList();
            _districts = Context.Districts.Where(x => !x.Deleted).ToList();
            _expluatationDates = Context.ExpluatationDates.Where(x => !x.Deleted).ToList();
            _prices = Context.PriceFilters.Where(x => !x.Deleted && x.AdvertType == Helpers.AdvertsType.NewBuilding).ToList();            
        }
    }
}