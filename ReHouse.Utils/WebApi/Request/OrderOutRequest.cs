using System;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.WebApi.Request
{
    public class OrderOutRequest : BaseRequest
    {
        public Int32 OrderId { get; set; }
        public OrderOutType OrderOutType { get; set; }
        public DateTime ShipingDate { get; set; }
        public DateTime? ReservedDate { get; set; }
        public Int32 Accounting { get; set; }
        public Int32 TargetId { get; set; }
        public Int32 AdressId { get; set; }
        public String Currency { get; set; }
        public String Comment { get; set; }
    }
}