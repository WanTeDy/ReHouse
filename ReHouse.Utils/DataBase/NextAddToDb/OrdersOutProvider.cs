using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.DataBase
{
    public class OrdersOutProvider : BaseObj
    {
        public Int32 orderID { get; set; }
        public Int32 ordererID { get; set; }
        public String type { get; set; }
        public String status { get; set; }
        public String currency { get; set; }
        public Int32 quantity { get; set; }
        public Decimal amount { get; set; }
        public Decimal actual_amount { get; set; }
        public Decimal volume { get; set; }
        public String delivery_type { get; set; }
        public Int32 addressID { get; set; }
        //public DateTime? subsidiaryID { get; set; }
        public Int32 targetID { get; set; }
        public DateTime reserveddate { get; set; }
        public Int32 reservedquantity { get; set; }
        public DateTime shipingdate { get; set; }
        public Int32 accounting { get; set; }
        public DateTime? closed { get; set; }
        public Int32? clientID { get; set; }
        public DateTime add_time { get; set; }
        public DateTime update_time { get; set; }
        public List<OrderComes> items { get; set; }

    }
}