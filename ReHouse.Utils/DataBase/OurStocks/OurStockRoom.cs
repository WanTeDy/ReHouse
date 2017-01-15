using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class OurStockRoom : BaseObj
    {
        public Int32 NumberOfStock { get; set; }
        [MaxLength(60)]
        public String Name { get; set; }
        [MaxLength(100)]
        public String Adress { get; set; }
        //public String Region { get; set; }
        public virtual List<UnitOfCommodity> UnitOfCommodities { get; set; }
    }
}