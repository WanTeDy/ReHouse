using System;
using System.Collections.Generic;
using System.Linq;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Districts
{
    public class UpdateDistrictOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public District _district { get; set; }

        public UpdateDistrictOperation(string tokenHash, District district)
        {
            _tokenHash = tokenHash;
            _district = district;
            RussianName = "Изменение района";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            var district = Context.Districts.FirstOrDefault(x => x.Id == _district.Id && !x.Deleted);
            if (district == null)
            {
                Errors.Add("Id", "*Район не найдена!");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(_district.RussianName))
                    Errors.Add("Title", "*Укажите название!");
                else
                {
                    district.RussianName = _district.RussianName;
                    district.ParrentId = _district.ParrentId;
                    Context.SaveChanges();
                }
            }
        }
    }
}