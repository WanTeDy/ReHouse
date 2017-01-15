using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Vendor
{
    public class AddItfamilyVendorOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private String VendorName { get; set; }
        private Int32 ItfamilyCategoryId { get; set; }

        public AddItfamilyVendorOperation(string tokenHash, string vendorName, int itfamilyCategoryId)
        {
            TokenHash = tokenHash;
            VendorName = vendorName;
            ItfamilyCategoryId = itfamilyCategoryId;
            RussianName = "Добавление производителя";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, Name, RussianName);

            if(ItfamilyCategoryId == 0)
                throw new ActionNotAllowedException("Поле ItfamilyCategoryId не может быть равно 0.");
            var vendor = new ItFamilyVendor
            {
                Name = VendorName,
                FromWhatProvider = FromWhatProvider.OurProduct,
            };
            var cat = Context.ItFamilyCategories.Include("ItFamilyVendors").FirstOrDefault(x => x.Id == ItfamilyCategoryId && !x.Deleted);
            if(cat == null)
                throw new ObjectNotFoundException("Категория IfamilyCategoryId не найдена. Id = " + ItfamilyCategoryId);
            if(cat.ItFamilyVendors.Any(x=>!x.Deleted && x.Name == VendorName))
                throw new ActionNotAllowedException("В данной категории уже присутствует такой производитель. Vendor Name = " + VendorName);

            cat.ItFamilyVendors.Add(vendor);
            Context.SaveChanges();
        }
    }
}