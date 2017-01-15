using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Currency
{
    public class GetCurrencyForPartnerOrClientsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<CurrenciesModel> CollectionCurrencies { get; set; }

        public GetCurrencyForPartnerOrClientsOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Проверка курса валюты для своих партнеров";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var colCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && x.Name == ConstV.CourseCash && !x.Deleted);
            List<DataBase.Currencies.Currency> cur2 = null;
            if (colCur.Any())
            {
                var maxDate = colCur.Max(x => x.DateTime);
                cur2 = colCur.Where(x => x.DateTime <= maxDate && x.DateTime >= maxDate.AddDays(-5)).ToList();
            }


            if (cur2 == null) return;
            var modelForUi = cur2.Select(collectionCurrency => new CurrenciesModel
            {
                Id = collectionCurrency.Id,
                Name = collectionCurrency.Name,
                Value = collectionCurrency.Value,
                TypeCurrency = ConstV.OurCurrencyName
            }).ToList();

            //var allCurrencies = Context.CollectionCurrencies.Where(x => !x.Deleted && x.Name == ConstV.OurCurrencyName).Include("Currencies").ToList();
            //foreach (var collectionCurrency in allCurrencies)
            //{
            //    var cur = collectionCurrency.Currencies.Where(y => !y.Deleted).Select(x => new CurrenciesModel
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Value = x.Value,
            //        //CurrencyId = x.CurrencyId,
            //        TypeCurrency = collectionCurrency.Name
            //    }).ToList();
            //    modelForUi.AddRange(cur);
            //}
            CollectionCurrencies = modelForUi;
        }
    }
}