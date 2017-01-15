using System;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.FrequencyPaymen
{
    public class AddFrequencyPaymenOperation : BaseOperation
    {
        public String NameFreq { get; set; }
        public String TokenHash { get; set; }

        public AddFrequencyPaymenOperation(string nameFreq, string tokenHash)
        {
            NameFreq = nameFreq;
            TokenHash = tokenHash;
            RussianName = "Добавление новой периодичности к услугам";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var freq = Context.FrequencyPayments.FirstOrDefault(x => x.Name == NameFreq && !x.Deleted);
            if(freq != null)
                throw new ExistsObjectException("Такое имя уже есть!");
            var newFreq = new FrequencyPayment { Name = NameFreq };
            Context.FrequencyPayments.Add(newFreq);
            Context.SaveChanges();
        }
    }
}