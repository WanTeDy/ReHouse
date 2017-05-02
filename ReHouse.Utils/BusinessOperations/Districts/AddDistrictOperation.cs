using System;
using System.Linq;
using ReHouse.Utils.Helpers;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.BusinessOperations.Districts
{
    public class AddDistrictOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        public District _district { get; set; }

        public AddDistrictOperation(string tokenHash, District district)
        {
            _tokenHash = tokenHash;
            _district = district;
            RussianName = "Добавление района";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);

            if (String.IsNullOrWhiteSpace(_district.RussianName))
                Errors.Add("Title", "*Укажите Название!");
            else
            {
                _district.Deleted = false;
                Context.Districts.Add(_district);
                Context.SaveChanges();
            }
        }
    }
}