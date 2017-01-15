using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ITfamily.Utils.Brain.Facade;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Currencies;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.Currency
{
    public class CheckNewCurrencyOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public List<CurrenciesModel> CollectionCurrencies { get; set; }
        public Boolean IsNewCurrency { get; set; }

        public CheckNewCurrencyOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            IsNewCurrency = false;
            RussianName = "Проверка нового курса валюты у поставщика";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var role =
                Context.RoleSet.FirstOrDefault(
                    x =>
                        !x.Deleted && !String.IsNullOrEmpty(x.ProviderLogin1) &&
                        !String.IsNullOrEmpty(x.ProviderMd5Password1));
            if(role==null) return;
            var auth = AuthBrainFacade.Auth(role.ProviderLogin1, role.ProviderMd5Password1).Result;
            if (auth != null && auth.status == 1)
            {
                var res = BrainCommonFacade.GetCurrencies(auth.result).Result;
                if (res != null)
                {
                    var brCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.Provider1 && !x.Deleted);
                    List<DataBase.Currencies.Currency> brainCur = null;
                    if (brCur.Any())
                    {
                        var maxDate = brCur.Max(x => x.DateTime);
                        brainCur = brCur.Where(x => x.DateTime == maxDate).ToList();
                    }
                    if (brainCur != null && brainCur.Any())
                    {
                        foreach (var currency in brainCur)
                        {
                            var curBr = res.FirstOrDefault(x => x.currencyID == currency.CurrencyId);
                            if (curBr != null)
                            {
                                if (Math.Abs(currency.Value - Convert.ToDecimal(curBr.value.Replace(",", "."))) > 0)
                                {
                                    var date = DateTime.Now;
                                    var curAdd = res.Select(x => new DataBase.Currencies.Currency
                                    {
                                        DateTime = date,
                                        Value = Convert.ToDecimal(x.value.Replace(",", ".")),
                                        BelongsCourse = "Поставщик 1",
                                        BelongsCourseType = "Поставщик 1",
                                        EnumBelongsType = BelongsType.Provider1,
                                        CurrencyId = x.currencyID,
                                        Name = x.name
                                    }).ToList();
                                    Context.Currencies.AddRange(curAdd);
                                    Context.SaveChanges();
                                    IsNewCurrency = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        var curAdd = res.Select(x => new DataBase.Currencies.Currency
                        {
                            DateTime = DateTime.Now,
                            Value = Convert.ToDecimal(x.value.Replace(",", ".")),
                            BelongsCourse = "Поставщик 1",
                            BelongsCourseType = "Поставщик 1",
                            EnumBelongsType = BelongsType.Provider1,
                            CurrencyId = x.currencyID,
                            Name = x.name
                        }).ToList();
                        Context.Currencies.AddRange(curAdd);
                        Context.SaveChanges();
                        IsNewCurrency = true;
                    }
                    //var brainCurrency = Context.CollectionCurrencies.Include("Currencies").FirstOrDefault(x => x.Type == "Brain");
                    //if (brainCurrency != null)
                    //{
                    //    if (brainCurrency.Currencies.Count > 0 )
                    //    {
                    //        foreach (var currency in brainCurrency.Currencies.Where(x=>!x.Deleted))
                    //        {
                    //            var curBr = res.FirstOrDefault(x => x.currencyID == currency.CurrencyId);
                    //            if (curBr != null)
                    //            {
                    //                //if(currency.Name != curBr.name)
                    //                //    currency.Name = curBr.name;
                    //                if (Math.Abs(currency.Value - Convert.ToDecimal(curBr.value.Replace(",", "."))) > 0)
                    //                {
                    //                    currency.Value = Convert.ToDecimal(curBr.value.Replace(",","."));
                    //                    Context.SaveChanges();
                    //                    IsNewCurrency = true;
                    //                }
                    //            }
                    //
                    //        }
                    //    }
                    //    else
                    //    {
                    //        var colCurrency = res.Select(x => new DataBase.Currencies.Currency
                    //        {
                    //            Name = x.name,
                    //            Value = Convert.ToDecimal(x.value.Replace(",",".")),
                    //            CurrencyId = x.currencyID,
                    //        }).ToList();
                    //        brainCurrency.Currencies.AddRange(colCurrency);
                    //        Context.SaveChanges();
                    //        IsNewCurrency = true;
                    //    }
                    //}
                }
            }
            if (IsNewCurrency)
            {
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
                CollectionCurrencies = GetCurrencyModel();
            }
        }

        private List<CurrenciesModel> GetCurrencyModel()
        {
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
                    TypeCurrency = collectionCurrency.BelongsCourse
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
                return modelForUi;
            }
            modelForUi.AddRange(cur2.Select(x => new CurrenciesModel
            {
                Id = x.Id,
                Name = x.Name,
                Value = x.Value,
                TypeCurrency = x.BelongsCourse
            }).ToList());
            return modelForUi;
        }
    }
}