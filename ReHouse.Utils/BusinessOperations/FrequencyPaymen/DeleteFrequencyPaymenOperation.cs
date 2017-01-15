using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.FrequencyPaymen
{
    public class DeleteFrequencyPaymenOperation : BaseOperation
    {
        public Int32 SelId { get; set; }
        public String TokenHash { get; set; }

        public DeleteFrequencyPaymenOperation(int selId, string tokenHash)
        {
            SelId = selId;
            TokenHash = tokenHash;
            RussianName = "Удаление конкретной периодичности для услуг";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var freq = Context.FrequencyPayments.FirstOrDefault(x => x.Id == SelId);
            if(freq == null)
                throw new ObjectNotFoundException("Такого обьекта нет! Id="+SelId);

            var service = Context.Serviceses.FirstOrDefault(x => x.FrequencyPaymentId == SelId && !x.Deleted);
            if (service == null)
                freq.Deleted = true;
            else
                throw new ExistsObjectException("Данную периодчиность оплаты нельзя удалить, так как у вас есть услуги с заданой периодичностью!");
            
            Context.SaveChanges();
        }
    }
}