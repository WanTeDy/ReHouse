using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Category
{
    public class UpdateItfamilyCategoryOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 ItfamilyCategoryId { get; set; }
        private String NewNameCategory { get; set; }

        public UpdateItfamilyCategoryOperation(string tokenHash, int itfamilyCategoryId, string newNameCategory)
        {
            TokenHash = tokenHash;
            ItfamilyCategoryId = itfamilyCategoryId;
            NewNameCategory = newNameCategory;
            RussianName = "Изменение названия категории";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var cat = Context.ItFamilyCategories.FirstOrDefault(x => x.Id == ItfamilyCategoryId && !x.Deleted);
            if (cat == null)
                throw new ObjectNotFoundException("Обьект ItfamilyCategoryId не найден. Id = " + ItfamilyCategoryId);

            var exCat = Context.ItFamilyCategories.FirstOrDefault(x => !x.Deleted && x.Name == NewNameCategory);
            if(exCat != null)
                throw new ActionNotAllowedException("Измените имя для данной категории. Такое имя уже присутствует.");

            cat.Name = NewNameCategory;
            Context.SaveChanges();
        }
    }
}