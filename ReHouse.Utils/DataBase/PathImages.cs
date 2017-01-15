using System;
using System.ComponentModel.DataAnnotations;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.DataBase
{
    public class PathImages : BaseObj
    {
        [MaxLength(128)]
        public String SmallImage { get; set; }
        [MaxLength(128)]
        public String BigImage { get; set; }
        public Int32? BrainProductId { get; set; }
        public BrainProduct BrainProduct { get; set; }
        public Int32? AdditionalDataId { get; set; }
        public AdditionalStockProductData AdditionalData { get; set; }
    }
}