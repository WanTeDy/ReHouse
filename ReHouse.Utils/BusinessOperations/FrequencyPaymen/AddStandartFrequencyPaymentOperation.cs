using System;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.FrequencyPaymen
{
    public class AddStandartFrequencyPaymentOperation : BaseOperation
    {
        public String TokenHash { get; set; }

        public AddStandartFrequencyPaymentOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Добавление стандарных периодичностей для услуг";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var freq1 = Context.FrequencyPayments.FirstOrDefault(x => x.Name == ConstV.EveryDayService && !x.Deleted);
            if (freq1 == null)
            {
                var freq = new FrequencyPayment {Name = ConstV.EveryDayService};
                Context.FrequencyPayments.Add(freq);
            }
            var freq2 = Context.FrequencyPayments.FirstOrDefault(x => x.Name == ConstV.MonthlyService && !x.Deleted);
            if (freq2 == null)
            {
                var freq = new FrequencyPayment { Name = ConstV.MonthlyService };
                Context.FrequencyPayments.Add(freq);
            }
            var freq3 = Context.FrequencyPayments.FirstOrDefault(x => x.Name == ConstV.OneTimeService && !x.Deleted);
            if (freq3 == null)
            {
                var freq = new FrequencyPayment { Name = ConstV.OneTimeService };
                Context.FrequencyPayments.Add(freq);
            }
            var freq4 = Context.FrequencyPayments.FirstOrDefault(x => x.Name == ConstV.QuarterlyService && !x.Deleted);
            if (freq4 == null)
            {
                var freq = new FrequencyPayment { Name = ConstV.QuarterlyService };
                Context.FrequencyPayments.Add(freq);
            }
            var freq5 = Context.FrequencyPayments.FirstOrDefault(x => x.Name == ConstV.WeeklyService && !x.Deleted);
            if (freq5 == null)
            {
                var freq = new FrequencyPayment { Name = ConstV.WeeklyService };
                Context.FrequencyPayments.Add(freq);
            }

            Context.SaveChanges();
        }
    }
}