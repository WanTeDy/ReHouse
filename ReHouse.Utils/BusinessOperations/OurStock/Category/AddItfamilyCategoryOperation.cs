using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Category
{
    public class AddItfamilyCategoryOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32? ItfamilyParentId { get; set; }
        private String NameCategory { get; set; }

        public AddItfamilyCategoryOperation(string tokenHash, int? itfamilyParentId, string nameCategory)
        {
            TokenHash = tokenHash;
            ItfamilyParentId = itfamilyParentId;
            NameCategory = nameCategory;
            RussianName = "Добавить свою категорию";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var cat = new ItFamilyCategory
            {
                ItFamilyParentId = ItfamilyParentId,
                Name = NameCategory,
                FromWhatProvider = FromWhatProvider.OurProduct,
            };
            var exCat = Context.ItFamilyCategories.FirstOrDefault(x => !x.Deleted && x.Name == NameCategory);
            if(exCat !=null)
                throw new ActionNotAllowedException("Вы не можете добавить такую категорию, так как уже есть такое имя категории.");

            if (ItfamilyParentId.HasValue && ItfamilyParentId.Value != 0)
            {
                var st = Context.StockProducts.FirstOrDefault(x => !x.Deleted && x.ItFamilyCategoryId == ItfamilyParentId.Value);
                if(st != null)
                    throw new ActionNotAllowedException("Вы не можете добавить в эту категорию подкатегорию, так как в ней присутствуют не удаленные товары. Переместите товары этой категории в другую категорию или удалите их.");
            }

            Context.ItFamilyCategories.Add(cat);
            Context.SaveChanges();
        }
    }
}