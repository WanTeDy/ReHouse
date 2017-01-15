using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations
{
    public class SelectTopProductForCategoryOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Int32 ProductId { get; set; }

        public SelectTopProductForCategoryOperation(string tokenHash, int productId)
        {
            TokenHash = tokenHash;
            ProductId = productId;
            RussianName = "Устанавливать TOP товар для категорий";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var prod = Context.StockProducts.FirstOrDefault(x => x.ProductId == ProductId);
            if(prod == null)
                throw new ObjectNotFoundException("Выбранный товар не найден.");
            var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == prod.ItFamilyCategoryId);
            if(cat == null)
                throw new ObjectNotFoundException("Выбранная категория не найдена");
            cat.BrainProduct = prod;
            Context.SaveChanges();
        }
    }
}