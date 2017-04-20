using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.Flat
{
    public class DeleteFlatOperation : BaseOperation //TODO
    {
        private String _tokenHash { get; set; }
        private Int32[] _advertsId { get; set; }

        public DeleteFlatOperation(string tokenHash, int[] advertsId)
        {
            _tokenHash = tokenHash;
            _advertsId = advertsId;
            RussianName = "Удаление объявлений";
        }

        protected override void InTransaction()
        {
            //var check = new CheckUserRoleAuthorityOperation(_tokenHash, Name, RussianName);
            if (_advertsId != null && _advertsId.Length > 0)
            {
                foreach (var advertId in _advertsId)
                {
                    var advert = Context.Adverts.FirstOrDefault(x => x.Id == advertId);
                    if (advert != null)
                        advert.Deleted = true;
                }
                Context.SaveChanges();
            }            
        }
    }
}