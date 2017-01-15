using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;

namespace ITfamily.Utils.BusinessOperations.RulesForPrices
{
    public class AddGlobalRulesForProductsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private List<RuleForPriceModel> RuleForPriceModels { get; set; }

        public AddGlobalRulesForProductsOperation(string tokenHash, List<RuleForPriceModel> ruleForPriceModels)
        {
            TokenHash = tokenHash;
            RuleForPriceModels = ruleForPriceModels;
            RussianName = "Добавление/изменение глобальной колекции правил на цены(менеджер или руководитель)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var rules = Context.RuleForPrices.Where(x => x.Category == null).ToList();

            foreach (var ruleForPriceModel in RuleForPriceModels)
            {
                if (ruleForPriceModel.Id != 0)
                {
                    var existRule = rules.FirstOrDefault(x => x.Id == ruleForPriceModel.Id);
                    if (existRule != null)
                        OurMaps.SetRuleForPriceFromModel(existRule, ruleForPriceModel);
                    else
                    {
                        var x = 0;
                    }
                }
                else
                {
                    Context.RuleForPrices.Add(OurMaps.ConvertFromModel(ruleForPriceModel));
                }
            }

            Context.SaveChanges();
        }
    }
}