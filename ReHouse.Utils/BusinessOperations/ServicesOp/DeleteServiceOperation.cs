using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.ServicesOp
{
    public class DeleteServiceOperation : BaseOperation
    {
        public Int32 SelId { get; set; }
        public String TokenHash { get; set; }

        public DeleteServiceOperation(int selId, string tokenHash)
        {
            SelId = selId;
            TokenHash = tokenHash;
            RussianName = "Удаление услуги";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var serv = Context.Serviceses.FirstOrDefault(x => x.Id == SelId);
            if(serv == null)
                throw new ObjectNotFoundException("Обьект не найден! Id=" + SelId);
            serv.Deleted = true;
            Context.SaveChanges();
        }
    }
}