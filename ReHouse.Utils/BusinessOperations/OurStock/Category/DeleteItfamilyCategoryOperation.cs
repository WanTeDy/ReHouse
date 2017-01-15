using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Category
{
    public class DeleteItfamilyCategoryOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 DeletedId { get; set; }

        public DeleteItfamilyCategoryOperation(string tokenHash, int deletedId)
        {
            TokenHash = tokenHash;
            DeletedId = deletedId;
            RussianName = "Удалить категорию";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var cat = Context.ItFamilyCategories.FirstOrDefault(x => !x.Deleted && x.Id == DeletedId);
            if(cat == null)
                throw new ObjectNotFoundException("Обьект ItfamilyCategory не найден. Id = " + DeletedId);
            if(cat.StockProducts != null && cat.StockProducts.Any(x=>!x.Deleted))
                throw new ActionNotAllowedException("Вы не можете удалить данную категорию, так как в ней присутствуют не удаленные товары.");
            cat.Deleted = true;
            Context.SaveChanges();
        }
    }
}