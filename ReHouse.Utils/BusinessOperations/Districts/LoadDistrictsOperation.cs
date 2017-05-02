using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Districts
{
    public class LoadDistrictsOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public List<District> _districts { get; set; }

        public LoadDistrictsOperation(string tokenHash)
        {
            _tokenHash = tokenHash;
            RussianName = "Получение всех районов";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            _districts = Context.Districts.Where(x => !x.Deleted).OrderBy(x => x.ParrentId).ToList();
        }
    }
}