using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.BusinessOperations.OurStock.StockProduct
{
    public class HelperPriceForListInOurStockRooms
    {
        private DataBase.Currencies.Currency _cur = null;
        private Contractor cont = null;
        private List<RuleForPrice> _ruleForPrices = null;
        private List<RuleForPrice> _ruleForPricesGlobal = null;
        //public List<BrainProductModel> BrainProductModels { get; set; }
        private DbItFamily Context;
        private Int32 CategoryId { get; set; }
        private String TokenHash { get; set; }

        public HelperPriceForListInOurStockRooms(string tokenHash, int categoryId, DbItFamily context)
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
            _ruleForPricesGlobal = Context.RuleForPrices.Include("ForWhom").Where(x => !x.Deleted && x.Category == null).ToList();
            _cur = CommonAccess.GetOurCourseCurrencies(Context);
            _ruleForPrices = Context.RuleForPrices.Include("ForWhom").Where(x => !x.Deleted && x.OurCategoryId == CategoryId).ToList();
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

        public List<DataBase.OurStocks.StockProduct> FormStockProductsList(
            List<DataBase.OurStocks.StockProduct> stockProducts)
        {
            foreach (var stockProduct in stockProducts)
            {
                if (IsEmployer(stockProduct)) continue;
                if ((_ruleForPrices == null || _ruleForPrices.Count <= 0) && (_ruleForPricesGlobal == null || _ruleForPricesGlobal.Count <= 0))
                {
                    stockProduct.Price = 0;
                    stockProduct.PriceUah = 0;
                    continue;
                }
                IsPartner(stockProduct);
                //if (IsPartner(stockProduct)) continue;
                var ruleCl = GetRule(stockProduct.Price, ConstV.RoleClient);
                if (ruleCl == null)
                {
                    stockProduct.Price = 0;
                    stockProduct.PriceUah = 0;
                    continue;
                }
                stockProduct.PriceUahForClients = FormPriceUah(stockProduct.Price, ruleCl);
                stockProduct.Price = 0;
                stockProduct.PriceUah = 0;
            }
            return stockProducts;
        }

        private bool IsEmployer(DataBase.OurStocks.StockProduct stockProduct)
        {
            if (cont == null || cont.Role.Name != ConstV.RoleAdministrator) return false;
            ////product.price = stockProduct.Price;
            ////product.price_uah = stockProduct.PriceUah;
            var rules = GetRules(stockProduct.Price);
            if (rules == null || rules.Count <= 0)
            {
                //BrainProductModels.Add(product);
                return true;
            }
            foreach (var ruleForPrice in rules)
            {
                if (ruleForPrice.ForWhom.Name == ConstV.RoleClient)
                    stockProduct.PriceUahForClients = FormPriceUah(stockProduct.Price, ruleForPrice);
                else if (ruleForPrice.ForWhom.Name == ConstV.RolePartner)
                    stockProduct.PriceUsdForPartner = FormPriceUsd(stockProduct.Price, ruleForPrice);
                else if (ruleForPrice.ForWhom.Name == ConstV.RoleManager)
                    stockProduct.PriceUsdForManager = FormPriceUsd(stockProduct.Price, ruleForPrice);
            }
            //BrainProductModels.Add(product);
            return true;
        }
        private bool IsPartner(DataBase.OurStocks.StockProduct stockProduct)
        {
            if (cont == null || cont.Role.Name == ConstV.RoleClient) return false;

            var rule = GetRule(stockProduct.Price, cont.RoleId);
            if (rule == null)
            {
                ////stockProduct.Price = 0;
                ////stockProduct.PriceUah = 0;
                //BrainProductModels.Add(product);
                return true;
            }
            if (cont.Role.Name == ConstV.RolePartner)
                stockProduct.PriceUsdForPartner = FormPriceUsd(stockProduct.Price, rule);
            else if (cont.Role.Name == ConstV.RoleManager)
                stockProduct.PriceUsdForManager = FormPriceUsd(stockProduct.Price, rule);

            ////stockProduct.Price = 0;
            ////stockProduct.PriceUah = 0;
            //BrainProductModels.Add(product);
            return true;
        }

        private List<RuleForPrice> GetRules(Decimal price)
        {
            List<RuleForPrice> rules = null;
            if (_ruleForPrices != null && _ruleForPrices.Count > 0)
                rules = _ruleForPrices.Where(x => x.From <= price && price < x.To && !x.Deleted).ToList();
            if ((rules == null || rules.Count <= 0) && (_ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0))
                rules = _ruleForPricesGlobal.Where(x => x.From <= price && price < x.To && !x.Deleted).ToList();
            return rules;
        }
        private Decimal FormPriceUsd(Decimal price, RuleForPrice ruleForPrice)
        {
            if (ruleForPrice.TypeRule == TypeRule.AddUsd)
                return (Math.Ceiling((price + ruleForPrice.ActionRule)*100)/100);
            if (ruleForPrice.TypeRule == TypeRule.MultiplePercent)
                return (Math.Ceiling((price * ruleForPrice.ActionRule)*100)/100);
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
            var ruleCl = _ruleForPrices.FirstOrDefault(x => x.From <= price && price < x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted);
            if (ruleCl == null && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            {
                ruleCl = _ruleForPricesGlobal.FirstOrDefault(x => x.From <= price && price < x.To
                                             && x.ForWhomId == forWhomId && !x.Deleted);
            }
            return ruleCl;
        }
        private RuleForPrice GetRule(Decimal price, String forWhom)
        {
            var ruleCl = _ruleForPrices.FirstOrDefault(x => x.From <= price && price < x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted);
            if (ruleCl == null && _ruleForPricesGlobal != null && _ruleForPricesGlobal.Count > 0)
            {
                ruleCl = _ruleForPricesGlobal.FirstOrDefault(x => x.From <= price && price < x.To
                                             && x.ForWhom.Name == forWhom && !x.Deleted);
            }
            return ruleCl;
        }
    }
}