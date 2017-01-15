using System;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class ProductModel
    {
        public Int32 ProductId { get; set; }
        public Decimal SoldPrice { get; set; }
        public Decimal SoldPriceUah { get; set; }
        public Int32 Quantity { get; set; }
        public String ProductName { get; set; }
        public String Image { get; set; }
    }
}