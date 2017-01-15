using System;
using System.Linq;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.BusinessOperations.OurStock.Vendor
{
    public class UpdateItfamilyVendorOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 ItfamilyVendorId { get; set; }
        private String NewVendorName { get; set; }

        public UpdateItfamilyVendorOperation(string tokenHash, int itfamilyVendorId, string newVendorName)
        {
            TokenHash = tokenHash;
            ItfamilyVendorId = itfamilyVendorId;
            NewVendorName = newVendorName;
            RussianName = "Изменить название производителя";
        }

        protected override void InTransaction()
        {
            CommonAccess.CheckContractorRoleAuthority(Context, TokenHash, NewVendorName, RussianName);

            if (ItfamilyVendorId == 0)
                throw new ActionNotAllowedException("Поле ItfamilyVendorId не может быть равно 0.");
            var vendor = Context.ItFamilyVendors.FirstOrDefault(x => x.Id == ItfamilyVendorId && !x.Deleted);
            if (vendor == null)
                throw new ObjectNotFoundException("Поле ItfamilyVendorId не найдено. Id = " + ItfamilyVendorId);
            var exVend = Context.ItFamilyVendors.FirstOrDefault(x => !x.Deleted && x.Name == NewVendorName);
            if(exVend != null)
                throw new ActionNotAllowedException("Новое имя производителе уже присутствует. Измените пожалуйста имя производителя на другое");

            vendor.Name = NewVendorName;
            Context.SaveChanges();
        }
    }
}