using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.RulesForPrices
{
    public class GetCollectionRulesPriceForCategoryOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 CategoryId { get; set; }
        public List<RuleForPriceModel> RuleForPrices { get; set; }
        public Decimal Min { get; set; }
        public Decimal Max { get; set; }
        public GetCollectionRulesPriceForCategoryOperation(string tokenHash, int categoryId)
        {
            TokenHash = tokenHash;
            CategoryId = categoryId;
            RussianName = "Получение колекции правил на цены для категории(менеджер или руководитель)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == CategoryId);
            if(cat == null)
                throw new ObjectNotFoundException("Выбраная категория не найдена");
            if (!cat.HasRule && cat.RulesForPrice.Count > 0)
            {
                cat.HasRule = true;
                Context.SaveChanges();
            }
            else if (!cat.HasRule && cat.RulesForPrice.Count <= 0)
            {
                RuleForPrices = null;
                //return;
            }
                //throw new ExistsObjectException("У выбраной категории нет правил цен.");
            if(RuleForPrices == null)
                RuleForPrices = Context.RuleForPrices.Where(x => !x.Deleted && x.OurCategoryId == cat.Id).Include("ForWhom").Select(OurMaps.ConvertToModel).ToList();
            if (!Context.StockProducts.Any()) return;
            Min = Context.StockProducts.Where(x => x.ItFamilyCategoryId == CategoryId).Min(x => x.Price);
            Max = Context.StockProducts.Where(x => x.ItFamilyCategoryId == CategoryId).Max(x => x.Price);
        }
    }
}