using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.RulesForPrices
{
    public class UpdateCollectionRulesPriceForCategoryOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private List<RuleForPriceModel> RuleForPrices { get; set; }
        private Int32 CategoryId { get; set; }

        public UpdateCollectionRulesPriceForCategoryOperation(string tokenHash, List<RuleForPriceModel> ruleForPrices, int categoryId)
        {
            TokenHash = tokenHash;
            RuleForPrices = ruleForPrices;
            CategoryId = categoryId;
            RussianName = "Изменение колекции правил на цены для категории(менеджер или руководитель)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == CategoryId);
            if (cat == null)
                throw new ObjectNotFoundException("Выбраная категория не найдена");
            if (!cat.HasRule && cat.RulesForPrice.Count <= 0)
            {
                cat.RulesForPrice.AddRange(RuleForPrices.Select(OurMaps.ConvertFromModel).ToList());
                cat.HasRule = true;
                Context.SaveChanges();
                return;
            }
            
            if (!cat.HasRule && cat.RulesForPrice.Count > 0)
            {
                cat.HasRule = true;
                Context.SaveChanges();
                cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == CategoryId);
                if (cat == null)
                    throw new ObjectNotFoundException("Выбраная категория не найдена");
            }
            //else if (!cat.HasRule && cat.RulesForPrice.Count <= 0)
            //    throw new ExistsObjectException("У выбраной категории нет правил цен.");
            
            foreach (var ruleForPrice in RuleForPrices)
            {
                RuleForPrice ruleForPriceInDb = null;
                if(ruleForPrice.Id != 0)
                    ruleForPriceInDb = cat.RulesForPrice.FirstOrDefault(x => x.Id == ruleForPrice.Id);
                if (ruleForPriceInDb == null)
                {
                    cat.RulesForPrice.Add(OurMaps.ConvertFromModel(ruleForPrice));
                    continue;
                }
                OurMaps.SetRuleForPriceFromModel(ruleForPriceInDb, ruleForPrice);
                if (!cat.HasRule) cat.HasRule = true;
            }
            if (cat.RulesForPrice.Count(x => !x.Deleted) <= 0) cat.HasRule = false;
            Context.SaveChanges();
        }
    }
}