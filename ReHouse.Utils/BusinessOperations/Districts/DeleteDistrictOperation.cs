using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Districts
{
    public class DeleteDistrictOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _districtsId { get; set; }

        public DeleteDistrictOperation(string tokenHash, int[] districtsId)
        {
            _tokenHash = tokenHash;
            _districtsId = districtsId;
            RussianName = "Удаление района";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_districtsId != null && _districtsId.Length > 0)
            {
                foreach (var districtId in _districtsId)
                {
                    var district = Context.Districts.FirstOrDefault(x => x.Id == districtId && !x.Deleted);
                    if (district != null)
                        district.Deleted = true;
                }
                Context.SaveChanges();
            }
        }
    }
}