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
    public class HelperPriceForListProduct
    {
        private Currency _cur = null;
        private Contractor cont = null;
        private List<RuleForPrice> _ruleForPrices = null;
        private List<RuleForPrice> _ruleForPricesGlobal = null;
        //public List<BrainProductModel> BrainProductModels { get; set; }
        private DbItFamily Context;
        private Int32 CategoryId { get; set; }
        private String TokenHash { get; set; }

        public HelperPriceForListProduct(string tokenHash, int categoryId, DbItFamily context)
        {
            TokenHash = tokenHash;
            CategoryId = categoryId;
            Context = context;
        }

        public void Init()
        {
            GetRulesAndCourse();
            CheckTokenHash();
        }
        private void GetRulesAndCourse()
        {
            //var cat = Context.ItFamilyCategories.Include("RulesForPrice").FirstOrDefault(x => x.Id == CategoryId);
            _ruleForPricesGlobal = Context.RuleForPrices.Include("ForWhom").Where(x => !x.Deleted && x.Category == null).ToList().Select(d=>new RuleForPrice
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
            _cur = CommonAccess.GetOurCourseCurrencies(Context);
            _ruleForPrices = Context.RuleForPrices.Include("ForWhom").Where(x => !x.Deleted && x.OurCategoryId == CategoryId).ToList().Select(d => new RuleForPrice
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
            //if (cat != null)
            //{
            //    //var colCur = Context.CollectionCurrencies.FirstOrDefault(x => x.Name == ConstV.OurCurrencyName);
            //    //if (colCur != null) _cur = Context.Currencies.FirstOrDefault(x => x.CollectionCurrencyId == colCur.Id && x.Name == ConstV.CourseCash);
            //    
            //    _ruleForPrices = Context.RuleForPrices.Include("ForWhom").Where(x => !x.Deleted && x.OurCategoryId == cat.Id).ToList();
            //}
        }

        private void CheckTokenHash()
        {
            if (!String.IsNullOrEmpty(TokenHash))
                cont = Context.Contractors.Include("Role")
                        .FirstOrDefault(x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
        }
        public BrainProductModel AddProductToList(StockProduct stockProduct, bool withDateModif)
        {
            var product = OurMaps.ConvertToModelForListStocks(stockProduct, false, true);
            product.price = 0;
            product.price_uah = 0;

            if (IsEmployer(stockProduct, product)) return product;
            if ((_ruleForPrices == null || _ruleForPrices.Count <= 0) && (_ruleForPricesGlobal == null || _ruleForPricesGlobal.Count <= 0) && !stockProduct.IsPriceForOneProduct)
            {
                //BrainProductModels.Add(product);
                return product;
            }
            if (IsPartner(stockProduct, product)) return product;
            if (stockProduct.IsPriceForOneProduct)
            {
                product.PriceUahForClients = Math.Ceiling(stockProduct.PriceUsdForClients * _cur.Value);
                product.price_uah = product.PriceUahForClients;
                return product;
            }
            var ruleCl = GetRule(stockProduct.Price, ConstV.RoleClient);
            //_ruleForPrices.FirstOrDefault(x => x.From <= brainProduct.price && brainProduct.price < x.To && x.ForWhom == ForWhom.Clients);
            if (ruleCl == null)
            {
                //BrainProductModels.Add(product);
                return product;
            }
            product.PriceUahForClients = FormPriceUah(stockProduct.Price, ruleCl);
            //product.PriceUsdForClients = FormPriceUsd(stockProduct.Price, ruleCl);
            return product;
            //BrainProductModels.Add(product);
        }
        private bool IsPartner(StockProduct stockProduct, BrainProductModel product)
        {
            if (cont == null || cont.Role.Name == ConstV.RoleClient) return false;

            if (stockProduct.IsPriceForOneProduct)
            {
                if (_cur != null)
                    product.PriceUahForClients = Math.Ceiling(stockProduct.PriceUsdForClients * _cur.Value);
                product.PriceUsdForClients = stockProduct.PriceUsdForClients;

                if (cont.Role.Name == ConstV.RolePartner || cont.Role.Name == ConstV.RoleManager)
                    product.PriceUsdForPartner = stockProduct.PriceUsdForPartner;
                if (cont.Role.Name == ConstV.RoleManager)
                    product.PriceUsdForManager = stockProduct.PriceUsdForManager;
                return true;
            }

            var rule = GetRule(stockProduct.Price, ConstV.RoleClient);
            if (rule != null)
            {
                product.PriceUsdForClients = FormPriceUsd(stockProduct.Price, rule);
                product.PriceUahForClients = FormPriceUah(stockProduct.Price, rule);
            }

            rule = GetRule(stockProduct.Price, cont.RoleId);
            if (rule == null)
            {
                //BrainProductModels.Add(product);
                return true;
            }
            if (cont.Role.Name == ConstV.RolePartner)
                product.PriceUsdForPartner = FormPriceUsd(stockProduct.Price, rule);
            else if (cont.Role.Name == ConstV.RoleManager)
            {
                product.PriceUsdForManager = FormPriceUsd(stockProduct.Price, rule);
                rule = GetRule(stockProduct.Price, ConstV.RolePartner);
                if (rule != null) product.PriceUsdForPartner = FormPriceUsd(stockProduct.Price, rule);
            }
            //BrainProductModels.Add(product);
            return true;
        }

        private List<RuleForPrice> GetRules(Decimal price)
        {
            List<RuleForPrice> rules = null;
            if (_ruleForPrices != null && _ruleForPrices.Count > 0)
            {
                rules = _ruleForPrices.Where(x => x.From <= price && price < x.To && !x.Deleted).ToList();
                rules = HelperFormRules(price, rules);
            }
            if ((rules == null || rules.Count <= 0) && (_ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0))
            {
                rules = _ruleForPricesGlobal.Where(x => x.From <= price && price < x.To && !x.Deleted).ToList();
                rules = HelperFormRules(price, rules);
            }
            if (_ruleForPrices != null && _ruleForPrices.Count > 0 && (rules == null || rules.Count <= 0))
            {
                rules = _ruleForPrices.Where(x => x.From <= price && 0 == x.To && !x.Deleted).ToList();
                rules = HelperFormRules(price, rules);
            }
            if ((rules == null || rules.Count <= 0) && (_ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0))
            {
                rules = _ruleForPricesGlobal.Where(x => x.From <= price && 0 == x.To && !x.Deleted).ToList();
                rules = HelperFormRules(price, rules);
            }
            return rules;
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

        private bool IsEmployer(StockProduct stockProduct, BrainProductModel product)
        {
            if (cont == null || cont.Role.Name != ConstV.RoleAdministrator) return false;
            product.price = stockProduct.Price;
            product.price_uah = stockProduct.PriceUah;
            if (stockProduct.IsPriceForOneProduct)
            {
                if (_cur != null)
                    product.PriceUahForClients = Math.Ceiling(stockProduct.PriceUsdForClients * _cur.Value);
                product.PriceUsdForClients = stockProduct.PriceUsdForClients;
                product.PriceUsdForPartner = stockProduct.PriceUsdForPartner;
                product.PriceUsdForManager = stockProduct.PriceUsdForManager;
                return true;
            }
            //if ((_ruleForPrices == null || _ruleForPrices.Count <= 0) && (_ruleForPricesGlobal == null || _ruleForPricesGlobal.Count<=0))
            //{
            //    BrainProductModels.Add(product);
            //    return true;
            //}
            var rules = GetRules(stockProduct.Price);
            //_ruleForPrices.Where(x => x.From <= brainProduct.price && brainProduct.price < x.To).ToList());
            if (rules == null || rules.Count <= 0)
            {
                //BrainProductModels.Add(product);
                return true;
            }
            foreach (var ruleForPrice in rules)
            {
                if (ruleForPrice.ForWhom.Name == ConstV.RoleClient)
                {
                    product.PriceUahForClients = FormPriceUah(stockProduct.Price, ruleForPrice);
                    product.PriceUsdForClients = FormPriceUsd(stockProduct.Price, ruleForPrice);
                }
                else if (ruleForPrice.ForWhom.Name == ConstV.RolePartner)
                    product.PriceUsdForPartner = FormPriceUsd(stockProduct.Price, ruleForPrice);
                else if (ruleForPrice.ForWhom.Name == ConstV.RoleManager)
                    product.PriceUsdForManager = FormPriceUsd(stockProduct.Price, ruleForPrice);
            }
            //BrainProductModels.Add(product);
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
        private Decimal FormPriceUah(Decimal price, RuleForPrice ruleForPrice)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd && _cur != null)
                return Math.Ceiling((price + ruleForPrice.ActionRule) * _cur.Value);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent && _cur != null)
                return Math.Ceiling((price * ruleForPrice.ActionRule) * _cur.Value);
            else
                return -1;
        }
        private RuleForPrice GetRule(Decimal price, Int32 forWhomId)
        {
            //var ruleCl = _ruleForPrices.FirstOrDefault(x => x.From <= price && price < x.To
            //                                 && x.ForWhomId == forWhomId && !x.Deleted);
            //if (ruleCl == null && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            //{
            //    ruleCl = _ruleForPricesGlobal.FirstOrDefault(x => x.From <= price && price < x.To
            //                                 && x.ForWhomId == forWhomId && !x.Deleted);
            //}
            //return ruleCl;
            var ruleCl = _ruleForPrices.Where(x => x.From <= price && price < x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted).ToList();
            if (!ruleCl.Any() && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            {
                ruleCl = _ruleForPricesGlobal.Where(x => x.From <= price && price < x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted).ToList();
            }
            if (!ruleCl.Any())
            {
                ruleCl = _ruleForPrices.Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted).ToList();
            }
            if (!ruleCl.Any() && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            {
                ruleCl = _ruleForPricesGlobal.Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted).ToList();
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
        private RuleForPrice GetRule(Decimal price, String forWhom)
        {
            //var ruleCl = _ruleForPrices.FirstOrDefault(x => x.From <= price && price < x.To
            //                                 && x.ForWhom.Name == forWhom && !x.Deleted);
            //if (ruleCl == null && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            //{
            //    ruleCl = _ruleForPricesGlobal.FirstOrDefault(x => x.From <= price && price < x.To
            //                                 && x.ForWhom.Name == forWhom && !x.Deleted);
            //}
            //return ruleCl;

            var ruleCl = _ruleForPrices.Where(x => x.From <= price && price < x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted).ToList();
            if (!ruleCl.Any() && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            {
                ruleCl = _ruleForPricesGlobal.Where(x => x.From <= price && price < x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted).ToList();
            }
            if (!ruleCl.Any())
            {
                ruleCl = _ruleForPrices.Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted).ToList();
            }
            if (!ruleCl.Any() && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            {
                ruleCl = _ruleForPricesGlobal.Where(x => x.From <= price && 0 == x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted).ToList();
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