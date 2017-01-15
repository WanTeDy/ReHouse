using System;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.DataBase.Filters
{
    public class ProductPropertyValues
    {
        public Int32 Id { get; set; }
        public virtual ProductProperty ProductProperty { get; set; }
        public Int32 PropertyId { get; set; }
        public virtual OurStocks.StockProduct StockProduct { get; set; }
        public Int32 StockProductId { get; set; }
        public String Value { get; set; }
        public Boolean Deleted { get; set; }
    }
}