using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Building
{
    public class DeleteNewBuildingOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _advertsId { get; set; }
        public Title _title { get; set; }

        public DeleteNewBuildingOperation(string tokenHash, int[] advertsId)
        {
            _tokenHash = tokenHash;
            _advertsId = advertsId;
            RussianName = "Удаление новостроек";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_advertsId != null && _advertsId.Length > 0)
            {
                foreach (var advertId in _advertsId)
                {
                    var advert = Context.NewBuildings.FirstOrDefault(x => x.Id == advertId);
                    if (advert != null)
                        advert.Deleted = true;
                }
                Context.SaveChanges();
            }
        }
    }
}