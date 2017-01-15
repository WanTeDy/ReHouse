using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.BusinessOperations;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Currencies;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.Brain.Helper
{
    public class HelperPriceForOneProduct
    {
        public BrainProductModel FormBrainProductModel(DbItFamily Context, StockProduct prod, Contractor cont)
        {
            var colCur = Context.Currencies.Where(x => x.EnumBelongsType == BelongsType.OurCource && x.Name == ConstV.CourseCash && !x.Deleted);
            Currency cur = null;
            if (colCur.Any())
            {
                var maxDate = colCur.Max(x => x.DateTime);
                cur = colCur.FirstOrDefault(x => x.DateTime == maxDate);
            }
            
            //if (colCur != null) cur = Context.Currencies.FirstOrDefault(x => x.CollectionCurrencyId == colCur.Id && x.Name == ConstV.CourseCash);
            if (prod == null) return null;

            var brainProductModel = OurMaps.ConvertToModel(prod, true);
            if (IsEmployer(cont, brainProductModel, prod, Context, cur)) return brainProductModel;

            brainProductModel.price = 0;
            brainProductModel.price_uah = 0;
            if (cont != null && cont.Role.Name != ConstV.RoleClient)
            {
                if (prod.IsPriceForOneProduct)
                {
                    if (cur != null)
                        brainProductModel.PriceUahForClients = Math.Ceiling(prod.PriceUsdForClients * cur.Value);
                    brainProductModel.PriceUsdForClients = prod.PriceUsdForClients;
                    brainProductModel.PriceUsdForPartner = prod.PriceUsdForPartner;
                    if(cont.Role.Name == ConstV.RoleManager)
                        brainProductModel.PriceUsdForManager = prod.PriceUsdForManager;
                    return brainProductModel;
                }
                var rule = GetRule(prod.Price, ConstV.RoleClient, Context, prod);
                if (rule != null)
                {
                    brainProductModel.PriceUsdForClients = FormPriceUsd(prod.Price, rule);
                    brainProductModel.PriceUahForClients = FormPriceUah(prod.Price, rule, cur);
                }

                rule = GetRule(prod.Price, cont.RoleId, Context, prod);
                if (rule == null) return brainProductModel;
                if (cont.Role.Name == ConstV.RolePartner)
                    brainProductModel.PriceUsdForPartner = FormPriceUsd(prod.Price, rule);
                else if (cont.Role.Name == ConstV.RoleManager)
                {
                    brainProductModel.PriceUsdForManager = FormPriceUsd(prod.Price, rule);
                    rule = GetRule(prod.Price, ConstV.RolePartner, Context, prod);
                    if (rule == null) return brainProductModel;
                    brainProductModel.PriceUsdForPartner = FormPriceUsd(prod.Price, rule);
                }

                return brainProductModel;
            }
            if (prod.IsPriceForOneProduct)
            {
                brainProductModel.PriceUahForClients = Math.Ceiling(prod.PriceUsdForClients * cur.Value);
                brainProductModel.price_uah = brainProductModel.PriceUahForClients;
            }
            var ruleCl = GetRule(prod.Price, ConstV.RoleClient, Context, prod);
            if (ruleCl == null) return brainProductModel;
            brainProductModel.PriceUahForClients = FormPriceUah(prod.Price, ruleCl, cur);
            brainProductModel.price_uah = brainProductModel.PriceUahForClients;
            return brainProductModel;
        }
        private bool IsEmployer(Contractor cont, BrainProductModel BrainProductModel, StockProduct prod, DbItFamily Context, Currency cur)
        {
            if (cont == null || cont.Role.Name != ConstV.RoleAdministrator) return false;
            BrainProductModel.price = prod.Price;
            BrainProductModel.price_uah = prod.PriceUah;

            if (prod.IsPriceForOneProduct)
            {
                if (cur != null)
                    BrainProductModel.PriceUahForClients = Math.Ceiling(prod.PriceUsdForClients * cur.Value);
                BrainProductModel.PriceUsdForClients = prod.PriceUsdForClients;
                BrainProductModel.PriceUsdForPartner = prod.PriceUsdForPartner;
                BrainProductModel.PriceUsdForManager = prod.PriceUsdForManager;
                return true;
            }
            var rules = GetRules(prod.Price, Context, prod);
            if (rules == null || rules.Count <= 0)
                return true;

            foreach (var ruleForPrice in rules)
            {
                if (ruleForPrice.ForWhom.Name == ConstV.RoleClient)
                {
                    BrainProductModel.PriceUahForClients = FormPriceUah(prod.Price, ruleForPrice, cur);
                    BrainProductModel.PriceUsdForClients = FormPriceUsd(prod.Price, ruleForPrice);
                }
                else if (ruleForPrice.ForWhom.Name == ConstV.RolePartner)
                    BrainProductModel.PriceUsdForPartner = FormPriceUsd(prod.Price, ruleForPrice);
                else if (ruleForPrice.ForWhom.Name == ConstV.RoleManager)
                    BrainProductModel.PriceUsdForManager = FormPriceUsd(prod.Price, ruleForPrice);
            }
            return true;
        }
        private Decimal FormPriceUsd(Decimal price, RuleForPrice ruleForPrice)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd)
                return (Math.Ceiling((price + ruleForPrice.ActionRule) * 100) / 100);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent)
                return (Math.Ceiling((price * ruleForPrice.ActionRule) * 100) / 100);
            else
                return -1;
        }
        private Decimal FormPriceUah(Decimal price, RuleForPrice ruleForPrice, Currency cur)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd && cur != null)
                return Math.Ceiling((price + ruleForPrice.ActionRule) * cur.Value);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent && cur != null)
                return Math.Ceiling((price * ruleForPrice.ActionRule) * cur.Value);
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
        private List<RuleForPrice> GetRules(Decimal price, DbItFamily Context, StockProduct prod)
        {
            var ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && price < x.To && !x.Deleted 
                && x.OurCategoryId == prod.ItFamilyCategoryId).ToList()
                .Select(d => new RuleForPrice
                {
                    Id = d.Id,
                    ActionRule = d.ActionRule,
                    ForWhom = d.ForWhom,
                    ForWhomId = d.ForWhomId,
                    From = d.From,
                    OurCategoryId = d.OurCategoryId,
                    To = d.To,
                    TypeRule = d.TypeRule
                }).ToList();
            ruleCl = HelperFormRules(price, ruleCl);
            
            if ((ruleCl == null || ruleCl.Count <= 0))
                ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && price < x.To && !x.Deleted && x.Category == null)
                    .ToList().Select(d => new RuleForPrice
                    {
                        Id = d.Id,
                        ActionRule = d.ActionRule,
                        ForWhom = d.ForWhom,
                        ForWhomId = d.ForWhomId,
                        From = d.From,
                        OurCategoryId = d.OurCategoryId,
                        To = d.To,
                        TypeRule = d.TypeRule
                    }).ToList();
            ruleCl = HelperFormRules(price, ruleCl);
            
            if ((ruleCl == null || ruleCl.Count <= 0))
                ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && 0 == x.To && !x.Deleted
                && x.OurCategoryId == prod.ItFamilyCategoryId).ToList()
                .Select(d => new RuleForPrice
                {
                    Id = d.Id,
                    ActionRule = d.ActionRule,
                    ForWhom = d.ForWhom,
                    ForWhomId = d.ForWhomId,
                    From = d.From,
                    OurCategoryId = d.OurCategoryId,
                    To = d.To,
                    TypeRule = d.TypeRule
                }).ToList();
            ruleCl = HelperFormRules(price, ruleCl);

            if ((ruleCl == null || ruleCl.Count <= 0))
                ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && 0 == x.To && !x.Deleted && x.Category == null)
                    .ToList().Select(d => new RuleForPrice
                    {
                        Id = d.Id,
                        ActionRule = d.ActionRule,
                        ForWhom = d.ForWhom,
                        ForWhomId = d.ForWhomId,
                        From = d.From,
                        OurCategoryId = d.OurCategoryId,
                        To = d.To,
                        TypeRule = d.TypeRule
                    }).ToList();
            ruleCl = HelperFormRules(price, ruleCl);

            return ruleCl;
        }
        private RuleForPrice GetRule(Decimal price, int forWhomId, DbItFamily Context, StockProduct prod)
        {
            //var ruleCl = Context.RuleForPrices.Include("ForWhom").FirstOrDefault(x => x.From <= price && price < x.To
            //                                                         && x.ForWhomId == forWhomId
            //                                                         && !x.Deleted && x.OurCategoryId == prod.ItFamilyCategoryId);
            //if (ruleCl == null)
            //    ruleCl =
            //        Context.RuleForPrices.FirstOrDefault(x => x.From <= price && price < x.To && x.ForWhomId == forWhomId
            //                                                        && !x.Deleted && x.Category == null);
            //return ruleCl;

            //var ruleCl = _ruleForPrices.FirstOrDefault(x => x.From <= price && price < x.To
            //                                 && x.ForWhomId == forWhomId && !x.Deleted);
            //if (ruleCl == null && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            //{
            //    ruleCl = _ruleForPricesGlobal.FirstOrDefault(x => x.From <= price && price < x.To
            //                                 && x.ForWhomId == forWhomId && !x.Deleted);
            //}
            //return ruleCl;
            var ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && price < x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted && x.OurCategoryId == prod.ItFamilyCategoryId).ToList();
            if (!ruleCl.Any())
            {
                ruleCl = Context.RuleForPrices.Where(x => x.From <= price && price < x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted && x.Category == null).ToList();
            }
            if (!ruleCl.Any())
            {
                Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted && x.OurCategoryId == prod.ItFamilyCategoryId).ToList();
            }
            if (!ruleCl.Any())
            {
                ruleCl = Context.RuleForPrices.Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted && x.Category == null).ToList();
            }
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
        private RuleForPrice GetRule(Decimal price, string forWhom, DbItFamily Context, StockProduct prod)
        {
            var ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && price < x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted && x.OurCategoryId == prod.ItFamilyCategoryId).ToList();
            if (!ruleCl.Any())
            {
                ruleCl = Context.RuleForPrices.Where(x => x.From <= price && price < x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted && x.Category == null).ToList();
            }
            if (!ruleCl.Any())
            {
                Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted && x.OurCategoryId == prod.ItFamilyCategoryId).ToList();
            }
            if (!ruleCl.Any())
            {
                ruleCl = Context.RuleForPrices.Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted && x.Category == null).ToList();
            }
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