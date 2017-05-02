using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Districts
{
    public class LoadDistrictOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32 _districtId { get; set; }
        public District _district { get; set; }

        public LoadDistrictOperation(string tokenHash, int districtId)
        {
            _tokenHash = tokenHash;
            _districtId = districtId;
            RussianName = "Получение района";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            _district = Context.Districts.FirstOrDefault(x => !x.Deleted && x.Id == _districtId);
        }
    }
}