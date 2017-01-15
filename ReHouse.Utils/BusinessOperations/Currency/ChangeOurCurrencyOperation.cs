using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Currency
{
    public class ChangeOurCurrencyOperation : BaseOperation
    {
        //public Int32 OurCurrencyCollectionId { get; set; }
        private String TokenHash { get; set; }
        private List<DataBase.Currencies.Currency> Currencies { get; set; }

        public ChangeOurCurrencyOperation(string tokenHash, List<DataBase.Currencies.Currency> currencies)
        {
            TokenHash = tokenHash;
            Currencies = currencies;
            RussianName = "Изменение валюты";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var date = DateTime.Now;
            foreach (var currency in Currencies)
            {
                var cur = Context.Currencies.FirstOrDefault(x => x.Id == currency.Id && !x.Deleted);
                if(cur == null)
                    throw new ObjectNotFoundException("Данные по курсу не могут быть изменены. \nОбьект не найден: " + currency.Id + " Name: " + currency.Name);
                //if (cur.Name != currency.Name)
                //    cur.Name = currency.Name;
                cur.Value = currency.Value;
                cur.DateTime = date;
            }
            Context.SaveChanges();
        }
    }
}