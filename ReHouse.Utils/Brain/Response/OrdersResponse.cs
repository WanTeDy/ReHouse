using System;
using System.Collections.Generic;
using ITfamily.Utils.Brain.Response.Models;

namespace ITfamily.Utils.Brain.Response
{
    public class OrdersResponse : BaseBrainResponse
    {
        public OrdersHelperResponse result { get; set; }
    }

    public class OrdersHelperResponse
    {
        public List<OrdersReport> list { get; set; }
        public Int32 count { get; set; }
    }
}