using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Currencies;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Currency
{
    public class GetCurrencyCollectionsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<CurrenciesModel> CollectionCurrencies { get; set; }

        public GetCurrencyCollectionsOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Получение своего курса валюты";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var colCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && !x.Deleted);
            List<DataBase.Currencies.Currency> cur2 = null;
            if (colCur.Any())
            {
                var maxDate = colCur.Max(x => x.DateTime);
                cur2 = colCur.Where(x => x.DateTime == maxDate).ToList();
            }

            List<CurrenciesModel> modelForUi = new List<CurrenciesModel>();
            if (cur2 != null)
                modelForUi.AddRange(cur2.Select(collectionCurrency => new CurrenciesModel
                {
                    Id = collectionCurrency.Id,
                    Name = collectionCurrency.Name,
                    Value = collectionCurrency.Value,
                    TypeCurrency = ConstV.OurCurrencyName
                }).ToList());

            colCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.Provider1 && !x.Deleted);
            cur2 = null;
            if (colCur.Any())
            {
                var maxDate = colCur.Max(x => x.DateTime);
                cur2 = colCur.Where(x => x.DateTime == maxDate).ToList();
            }
            if (cur2 == null)
            {
                CollectionCurrencies = modelForUi;
                return;
            }
            modelForUi.AddRange(cur2.Select(x=>new CurrenciesModel
            {
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                TypeCurrency = x.BelongsCourse
            }).ToList());

            //var allCurrencies = Context.CollectionCurrencies.Where(x=>!x.Deleted).Include("Currencies").ToList();
            //var modelForUi = new List<CurrenciesModel>();
            //foreach (var collectionCurrency in allCurrencies)
            //{
            //    var cur = collectionCurrency.Currencies.Where(y=>!y.Deleted).Select(x => new CurrenciesModel
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Value = x.Value,
            //        CurrencyId = x.CurrencyId,
            //        TypeCurrency = collectionCurrency.Name
            //    }).ToList();
            //    modelForUi.AddRange(cur);
            //}
            CollectionCurrencies = modelForUi;
        }
    }
}