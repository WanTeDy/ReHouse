using System;
using System.Linq;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations
{
    public class GetTopProductOperation : BaseOperation
    {
        public Int32 ProductId { get; set; }
        public String TokenHash { get; set; }
        public StockProduct StockProduct { get; set; }
        public GetTopProductOperation(int productId, string tokenHash)
        {
            ProductId = productId;
            TokenHash = tokenHash;
            RussianName = "Просмотр всей информации о TOP товаре (Менеджеры, Руководители)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var prod = Context.StockProducts.FirstOrDefault(x => x.ProductId == ProductId);
            if (prod == null)
                throw new ObjectNotFoundException("Выбранный товар не найден.");
            var cat = Context.ItFamilyCategories.Include("BrainProduct").FirstOrDefault(x => x.Id == prod.ItFamilyCategoryId);
            if (cat == null)
                throw new ObjectNotFoundException("Выбранная категория не найдена");
            if(cat.BrainProduct == null)
                throw new ItFamilyException("У выбранной категории отсуствует TOP товар");
            prod = Context.StockProducts.FirstOrDefault(x => x.ProductId == cat.BrainProduct.ProductId);
            if (prod == null)
                throw new ObjectNotFoundException("Выбранный TOP товар не найден.");
            StockProduct = new StockProduct
            {
                ProductId = prod.ProductId,
                Name = prod.Name,
                MainImage = prod.MainImage,
                BriefDescription = prod.BriefDescription
            };
        }
    }
}