using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.OurStock.Vendor
{
    public class LoadItfamilyVendorsOperation : BaseOperation
    {
        private String TokenHash { get; set; }
        private Int32 ItfamilyCategoryId { get; set; }
        public List<ItFamilyVendor> ItFamilyVendorsForOneCategory { get; set; }
        public List<ItFamilyVendor> AllItFamilyVendors { get; set; }

        public LoadItfamilyVendorsOperation(string tokenHash, int itfamilyCategoryId)
        {
            TokenHash = tokenHash;
            ItfamilyCategoryId = itfamilyCategoryId;
        }

        protected override void InTransaction()
        {
            var vendorForOneCat =
                Context.ItFamilyVendors.Where(
                    x => !x.Deleted && x.ItFamilyCategories.Any(d => !d.Deleted && d.Id == ItfamilyCategoryId)).ToList();
            ItFamilyVendorsForOneCategory = vendorForOneCat.Select(x => new ItFamilyVendor
            {
                Id = x.Id,
                CategoryId = x.Id,
                FromWhatProvider = x.FromWhatProvider,
                Name = x.Name,
                VendorId = x.VendorId,
            }).ToList();


            var allVendors =
                Context.ItFamilyVendors.Where(x => !x.Deleted).ToList();
            AllItFamilyVendors = allVendors.Select(x => new ItFamilyVendor
            {
                Id = x.Id,
                CategoryId = x.Id,
                FromWhatProvider = x.FromWhatProvider,
                Name = x.Name,
                VendorId = x.VendorId,
            }).ToList();
        }
    }
}