using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.DataBase
{
    public class OrderOut : BaseObj
    {
        public Int32 OrderId { get; set; }
        public Int32 OrderedId { get; set; }
        public OrderOutType OrderType { get; set; }
        [NotMapped]
        public String OrderTypeString { get; set; }
        public String Status { get; set; }
        [MaxLength(200)]
        public String TargetData { get; set; }
        public Int32 TargetId { get; set; }
        public Int32 AdressId { get; set; }
        [MaxLength(200)]
        public String AdressData { get; set; }
        //public PaymentStatus PaymentStatus { get; set; }
        [MaxLength(4)]
        public String Currency { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Quantity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ShipingDate { get; set; }
        [MaxLength(350)]
        public String PointOfDelivery { get; set; }
        public Int32 ContractorId { get; set; }
        [MaxLength(600)]
        public String Comment { get; set; }
        [MaxLength(400)]
        public String Notes { get; set; }
        public Boolean InStock { get; set; }
        public virtual List<OrdersItemForBrain> OrdersItems { get; set; }

        public OrderOut()
        {
            OrdersItems = new List<OrdersItemForBrain>();
        }
    }
}