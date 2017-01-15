using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.LegalEntities
{
    public class AddComesMoneyOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 PartnerId { get; set; }
        private CustomerCard CustomerCard { get; set; }

        public AddComesMoneyOperation(string tokenHash, int partnerId, CustomerCard customerCard)
        {
            TokenHash = tokenHash;
            PartnerId = partnerId;
            CustomerCard = customerCard;
            RussianName = "Дабавление поступающих денег на карточку клиента";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var partner = Context.Contractors.Include("CustomerCards").FirstOrDefault(x => !x.Deleted && x.Id == PartnerId && x.Role.Name == ConstV.RolePartner);
            if(partner == null)
                throw new ObjectNotFoundException("Данный партнер не найден");

            var courseCash = CommonAccess.GetOurCourseCash(Context);

            CustomerCard.DateTime = DateTime.UtcNow;
            CustomerCard.ComesMoney.DateTime = DateTime.UtcNow;
            CustomerCard.ContractorId = partner.Id;

            if (CustomerCard.ComesMoney.CurrencyType == CurrencyType.USD)
                CustomerCard.ComesUsd = CustomerCard.ComesMoney.Amount;
            else
                CustomerCard.ComesUsd = CustomerCard.ComesMoney.Amount / courseCash;

            var dec = Math.Floor(CustomerCard.ComesUsd.Value*100)/100;

            CustomerCard.Notes += " (" + dec.ToString("F") + " USD)";

            if (partner.CustomerCards != null && partner.CustomerCards.Count > 0)
            {
                var date = partner.CustomerCards.Max(x => x.DateTime);
                var cardDb = partner.CustomerCards.FirstOrDefault(x => x.DateTime == date);
                if (cardDb != null) CustomerCard.Balance = cardDb.Balance + CustomerCard.ComesUsd.Value;
            }
            else
                CustomerCard.Balance = CustomerCard.ComesUsd.Value;
            Context.CustomerCards.Add(CustomerCard);
            Context.SaveChanges();
        }
    }
}