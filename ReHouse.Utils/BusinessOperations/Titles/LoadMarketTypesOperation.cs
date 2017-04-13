using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Titles
{
    public class LoadMarketTypesOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<MarketType> _marketTypes { get; set; }

        public LoadMarketTypesOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение всех типов рынков объявлений";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var marketTypes = Context.MarketTypes.Where(x => !x.Deleted).ToList();
            _marketTypes = marketTypes.Select(x => new MarketType
            {
                Id = x.Id,
                RussianName = x.RussianName,
            }).ToList();
        }
    }
}