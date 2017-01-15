using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Response
{
    public class OurOrderResponse : BaseResponse
    {
        public OrderComes OrderComes { get; set; }
        public OrderOut OrderOut { get; set; }
        public List<OrderOut> OrdersOut { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<ProductModel> Basket { get; set; }
        public List<OrderComesModel> OrdersComeses { get; set; }
        public List<OrderCities> OrderCitieses { get; set; }
        public String FIO { get; set; }
        public String Email { get; set; }
        public String MobPhone { get; set; }
        public Boolean HasNew { get; set; }
        public Int32 CountPrepaidOrder { get; set; }
        public Int32 OrderId { get; set; }
        public String ContractorRole { get; set; }
        public Decimal AmountUah { get; set; }
        public Decimal AmountUsd { get; set; }
        public Boolean HasPrepaidOrder { get; set; }
    }
}