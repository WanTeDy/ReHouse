using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Clients
{
    public class DeleteClientOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Int32 DeleteId { get; set; }
        public DeleteClientOperation(int deleteId, String tokenHash)
        {
            DeleteId = deleteId;
            TokenHash = tokenHash;
            RussianName = "Удаление любого клиента";
        }

        protected override void InTransaction()
        {
            //var client = Context.ClientSet.FirstOrDefault(x => x.Id == DeleteId);
            var cont = Context.Contractors.FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            if(cont!=null && cont.Id != DeleteId)
                CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var client = Context.Contractors.FirstOrDefault(x => x.Id == DeleteId && !x.Deleted);

            if (client == null)
                throw new ObjectNotFoundException("Object Client not found id: " + DeleteId);

            client.Deleted = true;

            Context.SaveChanges();
        }
    }
}