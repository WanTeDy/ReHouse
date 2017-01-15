using System;

namespace ITfamily.Utils.DataBase
{
    public class OrdersOutItems : BaseObj
    {
        public Int32 productID { get; set; }
        public Int32 quantity { get; set; }
        public Decimal price { get; set; }
        public Decimal actual_price { get; set; }
        public Decimal invoice_price { get; set; }
        public DateTime delivery_time { get; set; }
        public Int32 reservedquantity { get; set; }
    }
}