using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.BusinessOperations.RulesForPrices
{
    public class GetNeedDataForGlobalRulesOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Decimal MinPriceUsd { get; set; }
        public Decimal MaxPriceUsd { get; set; }
        public List<RuleForPriceModel> RuleForPriceModels { get; set; }
        public GetNeedDataForGlobalRulesOperation(string tokenHash)
        {
            TokenHash = tokenHash;
            RussianName = "Получение глобальной колекции правил на цены для категории(менеджер или руководитель)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            if (!Context.StockProducts.Any()) return;
            var minPrice = Context.StockProducts.Where(x => !x.Deleted).Min(x => x.Price);
            var maxPrice = Context.StockProducts.Where(x => !x.Deleted).Max(x => x.Price);

            MinPriceUsd = minPrice;
            MaxPriceUsd = maxPrice;

            var rules = Context.RuleForPrices.Include("ForWhom").Where(x => x.Category == null && !x.Deleted).ToList();
            RuleForPriceModels = rules.Select(OurMaps.ConvertToModel).ToList();
        }
    }
}