using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.BusinessOperations;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.Currencies;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadBrainProductOperation : BaseOperation
    {
        private Int32 ProductId { get; set; }
        private String TokenHash { get; set; }
        public BrainProductModel BrainProductModel { get; set; }

        private Contractor cont = null;
        //private BrainProduct prod = null;
        //Currency _cur = null;
        
        public LoadBrainProductOperation(int productId, string tokenHash = "")
        {
            ProductId = productId;
            TokenHash = tokenHash;
            BrainProductModel = null;
        }

        protected override void InTransaction()
        {
            CheckTokenHash();

            var prod = Context.StockProducts.Include("AdditionalData").Include("AdditionalData.PathImageses").Include("PropertyValueses").FirstOrDefault(x => x.ProductId == ProductId && !x.Deleted);
            var helper = new HelperPriceForOneProduct();
            BrainProductModel = helper.FormBrainProductModel(Context, prod, cont);
            //var colCur = Context.CollectionCurrencies.FirstOrDefault(x => x.Name == ConstV.OurCurrencyName);
            //if (colCur != null) _cur = Context.Currencies.FirstOrDefault(x => x.CollectionCurrencyId == colCur.Id);
            //if (prod == null) return;
            //
            //BrainProductModel = OurMaps.ConvertToModel(prod, true);
            //if (IsEmployer()) return;
            //
            //BrainProductModel.price = 0;
            //BrainProductModel.price_uah = 0;
            //if (cont != null && cont.Role.Name != ConstV.RoleClient)
            //{
            //    var rule = GetRule(prod.price, cont.RoleId);
            //    if (rule == null) return;
            //    if (cont.Role.Name == ConstV.RolePartner)
            //        BrainProductModel.PriceUsdForPartner = FormPriceUsd(prod.price, rule);
            //    else if (cont.Role.Name == ConstV.RoleManager)
            //        BrainProductModel.PriceUsdForManager = FormPriceUsd(prod.price, rule);
            //    return;
            //}
            //var ruleCl = GetRule(prod.price, ConstV.RoleClient);
            //if (ruleCl == null) return;
            //BrainProductModel.PriceUahForClients = FormPriceUah(prod.price, ruleCl);
            //BrainProductModel.price_uah = BrainProductModel.PriceUahForClients;
        }
        
        //private bool IsEmployer()
        //{
        //    if (cont == null || cont.Role.Name != ConstV.RoleAdministrator) return false;
        //    BrainProductModel.price = prod.price;
        //    BrainProductModel.price_uah = prod.price_uah;
        //    
        //    var rules = GetRules(prod.price);
        //    if (rules == null || rules.Count <= 0)
        //        return true;
        //    
        //    foreach (var ruleForPrice in rules)
        //    {
        //        if (ruleForPrice.ForWhom.Name == ConstV.RoleClient)
        //            BrainProductModel.PriceUahForClients = FormPriceUah(prod.price, ruleForPrice);
        //        else if (ruleForPrice.ForWhom.Name == ConstV.RolePartner)
        //            BrainProductModel.PriceUsdForPartner = FormPriceUsd(prod.price, ruleForPrice);
        //        else if (ruleForPrice.ForWhom.Name == ConstV.RoleManager)
        //            BrainProductModel.PriceUsdForManager = FormPriceUsd(prod.price, ruleForPrice);
        //    }
        //    return true;
        //}

        private void CheckTokenHash()
        {
            if (!String.IsNullOrEmpty(TokenHash))
            {
                cont = Context.Contractors.Include("Role").FirstOrDefault(
                        x => x.TokenHash == TokenHash && !x.Deleted && x.IsActive);
            }
        }

        //private Decimal FormPriceUsd(Decimal price, RuleForPrice ruleForPrice)
        //{
        //    if (ruleForPrice.TypeRule == TypeRule.AddUsd)
        //        return (price + ruleForPrice.ActionRule);
        //    if (ruleForPrice.TypeRule == TypeRule.MultiplePercent)
        //        return (price * ruleForPrice.ActionRule);
        //    else
        //        return -1;
        //}
        //private Decimal FormPriceUah(Decimal price, RuleForPrice ruleForPrice, Currency cur)
        //{
        //    if (ruleForPrice.TypeRule == TypeRule.AddUsd && cur != null)
        //        return ((price + ruleForPrice.ActionRule) * cur.Value);
        //    if (ruleForPrice.TypeRule == TypeRule.MultiplePercent && cur != null)
        //        return ((price * ruleForPrice.ActionRule) * cur.Value);
        //    else
        //        return -1;
        //}
        //private List<RuleForPrice> GetRules(Decimal price)
        //{
        //    var ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && price < x.To
        //                                                             && !x.Deleted && x.BrainCategoryId==prod.BrainCategoryID).ToList();
        //    if ((ruleCl == null || ruleCl.Count <= 0))
        //        ruleCl = Context.RuleForPrices.Include("ForWhom").Where(x => x.From <= price && price < x.To 
        //                                                                && !x.Deleted && x.BrainCategory == null).ToList();
        //    return ruleCl;
        //}
        //private RuleForPrice GetRule(Decimal price, int forWhomId)
        //{
        //    var ruleCl = Context.RuleForPrices.Include("ForWhom").FirstOrDefault(x => x.From <= price && price < x.To
        //                                                             && x.ForWhomId == forWhomId
        //                                                             && !x.Deleted && x.BrainCategoryId == prod.BrainCategoryID);
        //    if (ruleCl == null)
        //        ruleCl =
        //            Context.RuleForPrices.FirstOrDefault(x => x.From <= price && price < x.To && x.ForWhomId == forWhomId 
        //                                                            && !x.Deleted && x.BrainCategory == null);
        //    return ruleCl;
        //}
        //private RuleForPrice GetRule(Decimal price, string forWhom)
        //{
        //    var ruleCl = Context.RuleForPrices.Include("ForWhom").FirstOrDefault(x => x.From <= price && price < x.To
        //                                                             && x.ForWhom.Name == forWhom
        //                                                             && !x.Deleted && x.BrainCategoryId == prod.BrainCategoryID);
        //    if (ruleCl == null)
        //        ruleCl =
        //            Context.RuleForPrices.FirstOrDefault(x => x.From <= price && price < x.To && x.ForWhom.Name == forWhom
        //                                                            && !x.Deleted && x.BrainCategory == null);
        //    return ruleCl;
        //}
    }
}