using System;
using System.Data;
using System.Linq;
using System.Text;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.BusinessOperations.News
{
    public class DeletePartnerOperation : BaseOperation
    {
        private String _tokenHash { get; set; }
        private Int32[] _partnersId { get; set; }

        public DeletePartnerOperation(string tokenHash, int[] partnersId)
        {
            _tokenHash = tokenHash;
            _partnersId = partnersId;
            RussianName = "Удаление партнеров";
        }

        protected override void InTransaction()
        {
            if (_partnersId != null && _partnersId.Length > 0)
            {
                foreach (var id in _partnersId)
                {
                    var partner = Context.Partners.FirstOrDefault(x => x.Id == id);
                    if (partner != null)
                        partner.Deleted = true;
                }
                Context.SaveChanges();
            }
        }
    }
}