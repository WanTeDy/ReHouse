using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Properties
{
    public class DeleteOnePropertyForStockProductOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        public Int32 DeletePropValueId { get; set; }

        public DeleteOnePropertyForStockProductOperation(string tokenHash, int deletePropValueId)
        {
            TokenHash = tokenHash;
            DeletePropValueId = deletePropValueId;
            RussianName = "Удаление значения свойства для товара(Характеристики)";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);
            var prop = Context.ProductPropertyValueses.FirstOrDefault(x => !x.Deleted && x.Id == DeletePropValueId);
            if (prop == null)
                throw new ObjectNotFoundException("Обьект ProductPropertyValue не найден Id = " + DeletePropValueId);
            prop.Deleted = true;
            Context.SaveChanges();
        }
    }
}