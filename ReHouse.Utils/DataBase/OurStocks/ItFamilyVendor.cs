using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class ItFamilyVendor : BaseObj
    {
        [MaxLength(40)]
        public String Name { get; set; }
        public Int32 VendorId { get; set; }
        [NotMapped]
        public Int32 CategoryId { get; set; }
        public virtual List<StockProduct> StockProducts { get; set; }
        public virtual List<ItFamilyCategory> ItFamilyCategories { get; set; }
        public FromWhatProvider FromWhatProvider { get; set; }
    }
}