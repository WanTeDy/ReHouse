using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Request
{
    public class OurOrderRequest : BaseRequest
    {
        public DeliveryType DeliveryType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String FIO { get; set; }
        public String Adress { get; set; }
        public String Comment { get; set; }
        public Int32 OrderOutId { get; set; }
        public Int32 SelectedId { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 OrderComeId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public OrderType OrderType { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DataPostOrder DataPostOrder { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}