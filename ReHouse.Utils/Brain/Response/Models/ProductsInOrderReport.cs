using System;

namespace ITfamily.Utils.Brain.Response.Models
{
    public class ProductsInOrderReport
    {
        public Int32 productID { get; set; }
        public Int32 quantity { get; set; }
        public Decimal price { get; set; }
        public Decimal actual_price { get; set; }
        public Decimal invoice_price { get; set; }
        public String delivery_time { get; set; }
        public Int32 reservedquantity { get; set; }
    }
}