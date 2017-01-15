using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations.BussOpWithDapper.Helpers
{
    public class HelperFormPriceForModelCategories
    {
        public DataBase.Currencies.Currency Currency { get; set; }
        public List<RuleForPrice> RuleForPrices { get; set; }
        public HelperFormPriceForModelCategories(DbItFamily Context, Contractor cont)
        {
            if (cont != null)
            {
                var rules =
                    Context.RuleForPrices.Include("ForWhom")
                        .Where(x => !x.Deleted && x.ForWhomId == cont.RoleId)
                        .ToList();
                RuleForPrices = rules.Select(x => new RuleForPrice
                {
                    Id = x.Id,
                    ActionRule = x.ActionRule,
                    ForWhom = new Role {Id = x.ForWhomId, Name = x.ForWhom.Name},
                    ForWhomId = x.ForWhomId,
                    From = x.From,
                    To = x.To,
                    TypeRule = x.TypeRule,
                    OurCategoryId = x.OurCategoryId
                }).ToList();
            }
            else
            {
                var rules =
                    Context.RuleForPrices.Include("ForWhom")
                        .Where(x => !x.Deleted && x.ForWhom.Name == ConstV.RoleClient)
                        .ToList();
                RuleForPrices = rules.Select(x => new RuleForPrice
                {
                    Id = x.Id,
                    ActionRule = x.ActionRule,
                    ForWhom = new Role { Id = x.ForWhomId, Name = x.ForWhom.Name },
                    ForWhomId = x.ForWhomId,
                    From = x.From,
                    To = x.To,
                    TypeRule = x.TypeRule,
                    OurCategoryId = x.OurCategoryId
                }).ToList();
            }
            var colCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && x.Name == ConstV.CourseCash && !x.Deleted);
            DataBase.Currencies.Currency cur = null;
            if (colCur.Any())
            {
                var maxDate = colCur.Max(x => x.DateTime);
                cur = colCur.FirstOrDefault(x => x.DateTime == maxDate);
            }
            Currency = cur;
        }

        /// <summary>
        /// Может использоваться для ItfamilyManager и Для остальных вещей, где не нужно знать поля Images
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="prod">BrainProductModel</param>
        /// <param name="cont"></param>
        /// <returns></returns>
        public BrainProductModel FormBrainProductModel(DbItFamily Context, BrainProductModel prod, Contractor cont)
        {
            if (prod == null) return null;
            //var colCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && x.Name == ConstV.CourseCash && !x.Deleted);
            //Currency cur = null;
            //if (colCur.Any())
            //{
            //    var maxDate = colCur.Max(x => x.DateTime);
            //    cur = colCur.FirstOrDefault(x => x.DateTime == maxDate);
            //}

            //if (colCur != null) cur = Context.Currencies.FirstOrDefault(x => x.CollectionCurrencyId == colCur.Id && x.Name == ConstV.CourseCash);
            

            //var brainProductModel = OurMaps.ConvertToModel(prod, true);
            if (IsEmployer(cont, prod, Context, Currency)) return prod;
            if (prod.IsPriceForOneProduct)
            {
                if (Currency != null)
                    prod.PriceUahForClients = Math.Ceiling(prod.PriceUsdForClients * Currency.Value);
                //return prod;
            }
            if (cont != null && cont.Role.Name != ConstV.RoleClient)
            {
                if (cont.Role.Name == ConstV.RolePartner && prod.IsPriceForOneProduct)
                {
                    prod.PriceUsdForManager = 0;
                    prod.price_uah = 0;
                    prod.price = 0;
                    return prod;
                }
                if (cont.Role.Name == ConstV.RoleManager && prod.IsPriceForOneProduct)
                {
                    prod.price_uah = 0;
                    prod.price = 0;
                    return prod;
                }

                var rule = GetRule(prod.price, cont.RoleId, Context, prod);
                if (rule == null)
                {
                    prod.price = 0;
                    prod.price_uah = 0;
                    return prod;
                }
                if (cont.Role.Name == ConstV.RolePartner)
                    prod.PriceUsdForPartner = FormPriceUsd(prod.price, rule);
                else if (cont.Role.Name == ConstV.RoleManager)
                    prod.PriceUsdForManager = FormPriceUsd(prod.price, rule);
                prod.price = 0;
                prod.price_uah = 0;
                return prod;
            }
            if (prod.IsPriceForOneProduct)
            {
                prod.PriceUsdForPartner = 0;
                prod.PriceUsdForManager = 0;
                prod.price_uah = 0;
                prod.price = 0;
                return prod;
            }
            var ruleCl = GetRule(prod.price, ConstV.RoleClient, Context, prod);
            if (ruleCl == null)
            {
                prod.price = 0;
                prod.price_uah = 0;
                return prod;
            }
            prod.PriceUahForClients = FormPriceUah(prod.price, ruleCl, Currency);
            prod.price_uah = prod.PriceUahForClients;
            prod.price = 0;
            return prod;
        }
        private bool IsEmployer(Contractor cont, BrainProductModel prod, DbItFamily Context, DataBase.Currencies.Currency cur)
        {
            if (cont == null || cont.Role.Name != ConstV.RoleAdministrator) return false;

            if (prod.IsPriceForOneProduct)
            {
                if (cur != null)
                    prod.PriceUahForClients = Math.Ceiling(prod.PriceUsdForClients*cur.Value);
                return true;
            }

            var rules = GetRules(prod.price, Context, prod);
            if (rules == null || rules.Count <= 0)
                return true;

            foreach (var ruleForPrice in rules)
            {
                if (ruleForPrice.ForWhom.Name == ConstV.RoleClient)
                    prod.PriceUahForClients = FormPriceUah(prod.price, ruleForPrice, cur);
                else if (ruleForPrice.ForWhom.Name == ConstV.RolePartner)
                    prod.PriceUsdForPartner = FormPriceUsd(prod.price, ruleForPrice);
                else if (ruleForPrice.ForWhom.Name == ConstV.RoleManager)
                    prod.PriceUsdForManager = FormPriceUsd(prod.price, ruleForPrice);
            }
            return true;
        }
        private Decimal FormPriceUsd(Decimal price, RuleForPrice ruleForPrice)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd)
                return (price + ruleForPrice.ActionRule);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent)
                return (price * ruleForPrice.ActionRule);
            else
                return -1;
        }
        private Decimal FormPriceUah(Decimal price, RuleForPrice ruleForPrice, DataBase.Currencies.Currency cur)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd && cur != null)
                return ((price + ruleForPrice.ActionRule) * cur.Value);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent && cur != null)
                return ((price * ruleForPrice.ActionRule) * cur.Value);
            else
                return -1;
        }
        public RuleForPrice GetMaxRuleForPriceFromListRules(string forWhom, decimal price, List<RuleForPrice> rules)
        {
            var prices = new List<HelperFormPrice>();
            if (rules.Any())
                foreach (var ruleForPrice in rules.Where(x => x.ForWhom.Name == forWhom))
                {
                    var priceForOne = FormPriceUsd(price, ruleForPrice);
                    prices.Add(new HelperFormPrice
                    {
                        Price = priceForOne,
                        RuleForPrice = ruleForPrice
                    });
                }
            if (prices.Any())
            {
                var maxPrice = prices.Max(x => x.Price);
                var maxRule = prices.FirstOrDefault(x => x.Price == maxPrice);
                if (maxRule != null)
                    return maxRule.RuleForPrice;
            }
            return null;
        }
        private List<RuleForPrice> HelperFormRules(decimal price, List<RuleForPrice> rules)
        {
            if (rules.Any())
            {
                var newRules = new List<RuleForPrice>();
                var rule = GetMaxRuleForPriceFromListRules(ConstV.RoleManager, price, rules);
                if (rule != null)
                    newRules.Add(rule);
                rule = GetMaxRuleForPriceFromListRules(ConstV.RolePartner, price, rules);
                if (rule != null)
                    newRules.Add(rule);
                rule = GetMaxRuleForPriceFromListRules(ConstV.RoleClient, price, rules);
                if (rule != null)
                    newRules.Add(rule);
                rules = newRules;
            }
            return rules;
        }
        private List<RuleForPrice> GetRules(Decimal price, DbItFamily Context, BrainProductModel prod)
        {
            var ruleCl = RuleForPrices.Where(x => x.From <= price && price < x.To
                                                && !x.Deleted && x.OurCategoryId.HasValue && x.OurCategoryId.Value == prod.ItfamilyCategoryID).ToList();
            ruleCl = HelperFormRules(price, ruleCl);
            if ((ruleCl == null || ruleCl.Count <= 0))
            {
                ruleCl = RuleForPrices.Where(x => x.From <= price && price < x.To
                                                                        && !x.Deleted && !x.OurCategoryId.HasValue).ToList();
                ruleCl = HelperFormRules(price, ruleCl);
            }
            if ((ruleCl == null || ruleCl.Count <= 0))
            {
                RuleForPrices.Where(x => x.From <= price && 0 == x.To
                                                && !x.Deleted && x.OurCategoryId.HasValue && x.OurCategoryId.Value == prod.ItfamilyCategoryID).ToList();
                ruleCl = HelperFormRules(price, ruleCl);
            }
            if ((ruleCl == null || ruleCl.Count <= 0))
            {
                ruleCl = RuleForPrices.Where(x => x.From <= price && 0 == x.To
                                                                        && !x.Deleted && !x.OurCategoryId.HasValue).ToList();
                ruleCl = HelperFormRules(price, ruleCl);
            }
            return ruleCl;
        }
        private RuleForPrice GetRule(Decimal price, int forWhomId, DbItFamily Context, BrainProductModel prod)
        {
            var ruleCl = RuleForPrices.Where(x => x.From <= price && price < x.To
                                                            && x.ForWhomId == forWhomId && !x.Deleted 
                                                            && x.OurCategoryId.HasValue && x.OurCategoryId.Value == prod.ItfamilyCategoryID).ToList();
            if (!ruleCl.Any())
                ruleCl = RuleForPrices.Where(x => x.From <= price && price < x.To && x.ForWhomId == forWhomId
                                                                    && !x.Deleted && !x.OurCategoryId.HasValue).ToList();
            if (!ruleCl.Any())
                ruleCl = RuleForPrices.Where(x => x.From <= price && 0 == x.To
                                                            && x.ForWhomId == forWhomId && !x.Deleted
                                                            && x.OurCategoryId.HasValue && x.OurCategoryId.Value == prod.ItfamilyCategoryID).ToList();
            if (!ruleCl.Any())
                ruleCl = RuleForPrices.Where(x => x.From <= price && 0 == x.To && x.ForWhomId == forWhomId
                                                                    && !x.Deleted && !x.OurCategoryId.HasValue).ToList();

            var prices = new List<HelperFormPrice>();
            if (!ruleCl.Any()) return null;
            foreach (var ruleForPrice in ruleCl)
            {
                var priceForOne = FormPriceUsd(price, ruleForPrice);
                prices.Add(new HelperFormPrice
                {
                    Price = priceForOne,
                    RuleForPrice = ruleForPrice
                });
            }
            if (prices.Any())
            {
                var maxPrice = prices.Max(x => x.Price);
                var maxRule = prices.FirstOrDefault(x => x.Price == maxPrice);
                if (maxRule != null)
                    return maxRule.RuleForPrice;
            }
            return null;
        }
        private RuleForPrice GetRule(Decimal price, string forWhom, DbItFamily Context, BrainProductModel prod)
        {
            var ruleCl = RuleForPrices.Where(x => x.From <= price && price < x.To
                                                    && x.ForWhom.Name == forWhom && !x.Deleted 
                                                    && x.OurCategoryId.HasValue && x.OurCategoryId.Value == prod.ItfamilyCategoryID).ToList();
            if (!ruleCl.Any())
                ruleCl =
                    Context.RuleForPrices.Where(x => x.From <= price && price < x.To && x.ForWhom.Name == forWhom
                                                                    && !x.Deleted && !x.OurCategoryId.HasValue).ToList();

            if (!ruleCl.Any())
                ruleCl = RuleForPrices.Where(x => x.From <= price && 0 == x.To
                                                    && x.ForWhom.Name == forWhom && !x.Deleted
                                                    && x.OurCategoryId.HasValue && x.OurCategoryId.Value == prod.ItfamilyCategoryID).ToList();
            if (!ruleCl.Any())
                ruleCl =
                    Context.RuleForPrices.Where(x => x.From <= price && 0 == x.To && x.ForWhom.Name == forWhom
                                                                    && !x.Deleted && !x.OurCategoryId.HasValue).ToList();

            var prices = new List<HelperFormPrice>();
            if (!ruleCl.Any()) return null;
            foreach (var ruleForPrice in ruleCl)
            {
                var priceForOne = FormPriceUsd(price, ruleForPrice);
                prices.Add(new HelperFormPrice
                {
                    Price = priceForOne,
                    RuleForPrice = ruleForPrice
                });
            }
            if (prices.Any())
            {
                var maxPrice = prices.Max(x => x.Price);
                var maxRule = prices.FirstOrDefault(x => x.Price == maxPrice);
                if (maxRule != null)
                    return maxRule.RuleForPrice;
            }
            return null;
        } 
    }
}