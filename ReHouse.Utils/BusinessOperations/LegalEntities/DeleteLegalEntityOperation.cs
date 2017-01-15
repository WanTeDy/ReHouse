using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.LegalEntities
{
    public class DeleteLegalEntityOperation : BaseOperation
    {
        public Int32 DeleteId { get; set; }
        public String TokenHash { get; set; }

        public DeleteLegalEntityOperation(int deleteId, string tokenHash)
        {
            DeleteId = deleteId;
            TokenHash = tokenHash;
            RussianName = "Удаление партнера";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var legal = Context.Contractors.FirstOrDefault(x => x.Id == DeleteId);
            if (legal == null)
                throw new ObjectNotFoundException("Object Entrepreneur not found id: " + DeleteId);
            legal.Deleted = true;
            Context.SaveChanges();
        }
    }
}