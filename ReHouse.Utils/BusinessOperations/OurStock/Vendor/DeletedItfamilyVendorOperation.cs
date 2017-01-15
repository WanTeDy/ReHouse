using System;
using System.Linq;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Vendor
{
    public class DeletedItfamilyVendorOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 DeletedId { get; set; }

        public DeletedItfamilyVendorOperation(string tokenHash, int deletedId)
        {
            TokenHash = tokenHash;
            DeletedId = deletedId;
            RussianName = "Удаление производителя";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            var vendor = Context.ItFamilyVendors.FirstOrDefault(x => !x.Deleted && x.Id == DeletedId);
            if(vendor == null)
                throw new ObjectNotFoundException("Обьект ItFamilyVendor не найден. Id = " + DeletedId);
            if (vendor.StockProducts != null && vendor.StockProducts.Any(x => !x.Deleted))
                throw new ActionNotAllowedException("Вы не можете удалить данного производителя, так как в нем присутствуют не удаленные товары.");
            vendor.Deleted = true;
            Context.SaveChanges();
        }
    }
}