using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Employeers
{
    public class DeleteEmployerOperation : BaseOperation
    {
        public Int32 DeleteId { get; set; }
        public String TokenHash { get; set; }
        public DeleteEmployerOperation(int deleteId, string tokenHash)
        {
            DeleteId = deleteId;
            TokenHash = tokenHash;
            RussianName = "Удаление работника";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var empl = Context.Contractors.FirstOrDefault(x => x.Id == DeleteId);
            if(empl == null)
                throw new ObjectNotFoundException("Работник не найден! Id = " + DeleteId);
            empl.Deleted = true;
            Context.SaveChanges();
        }
    }
}