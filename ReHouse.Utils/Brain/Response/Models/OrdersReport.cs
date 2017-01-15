using System;
using System.Collections.Generic;

namespace ITfamily.Utils.Brain.Response.Models
{
    public class OrdersReport
    {
        public String orderID { get; set; }
        public String ordererID { get; set; }
        public String type { get; set; }
        public String status { get; set; }
        public String currency { get; set; }
        public Int32 quantity { get; set; }
        public Decimal amount { get; set; }
        public Decimal actual_amount { get; set; }
        public Double volume { get; set; }
        public String delivery_type { get; set; }
        public String addressID { get; set; }
        public Int32? subsidiaryID { get; set; }
        public Int32 targetID { get; set; }
        public String reserveddate { get; set; }
        public Int32 reservedquantity { get; set; }
        public String shipingdate { get; set; }
        public String accounting { get; set; }
        //public String closed { get; set; }
        public String clientID { get; set; }
        public String add_time { get; set; }
        public String update_time { get; set; }
        public List<ProductsInOrderReport> items { get; set; }
    }
}