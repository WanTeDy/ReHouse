using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.FrequencyPaymen
{
    public class UpdateFrequencyPaymenOperation : BaseOperation
    {
        public Int32 SelId { get; set; }
        public String NameFreq { get; set; }
        public String TokenHash { get; set; }

        public UpdateFrequencyPaymenOperation(int selId, string nameFreq, string tokenHash)
        {
            SelId = selId;
            NameFreq = nameFreq;
            TokenHash = tokenHash;
            RussianName = "Изменение периодичности для услуг";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var freq = Context.FrequencyPayments.FirstOrDefault(x => x.Id == SelId);
            if(freq == null)
                throw new ObjectNotFoundException("Обьект не найден");
            freq.Name = NameFreq;
            Context.SaveChanges();
        }
    }
}