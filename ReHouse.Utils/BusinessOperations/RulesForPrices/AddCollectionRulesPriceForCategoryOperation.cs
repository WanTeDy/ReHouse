using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.RulesForPrices
{
    public class AddCollectionRulesPriceForCategoryOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 CategoryId { get; set; }
        private List<RuleForPriceModel> RuleForPrices { get; set; }

        public AddCollectionRulesPriceForCategoryOperation(string tokenHash, int categoryId, List<RuleForPriceModel> ruleForPrices)
        {
            TokenHash = tokenHash;
            CategoryId = categoryId;
            RuleForPrices = ruleForPrices;
            RussianName = "Добавление колекции правил на цены для категории(менеджер или руководитель)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == CategoryId);
            if (cat == null)
                throw new ObjectNotFoundException("Выбраная категория не найдена");
            if (!cat.HasRule && cat.RulesForPrice.Count > 0)
            {
                cat.HasRule = true;
                Context.SaveChanges();
            }
            else if (!cat.HasRule && cat.RulesForPrice.Count <= 0)
            {
                cat.RulesForPrice.AddRange(RuleForPrices.Select(OurMaps.ConvertFromModel).ToList());
                cat.HasRule = true;
                Context.SaveChanges();
            }
        }
    }
}