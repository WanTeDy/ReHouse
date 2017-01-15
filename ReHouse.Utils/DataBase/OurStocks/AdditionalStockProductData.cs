using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class AdditionalStockProductData
    {
        public Int32 Id { get; set; }
        [MaxLength(20)]
        public String ProductCode { get; set; }
        public Double Volume { get; set; }
        [MaxLength(8000)]
        public String Description { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateAdded { get; set; }
        //TODO customize one to many For AdditionalStockProductData to PathImages
        public virtual List<PathImages> PathImageses { get; set; }
        public virtual StockProduct StockProduct { get; set; }
    }
}