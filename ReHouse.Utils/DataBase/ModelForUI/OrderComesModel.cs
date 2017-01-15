using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class OrderComesModel
    {
        public Int32 Id { get; set; }
         public String OrderType { get; set; }
        
        //добавить в бизнесс операцию проверку актуальности наличия на удаленном складе
        //public Boolean IsExistsInOtherStock { get; set; }
        
        //TODO в бизнесс операции учет статусов оплачен, предоплата, частичный аванс, или кредитная линия
        public String PaymentStatus { get; set; }
        public Int32 ContractorId { get; set; }
        public String Currency { get; set; }
        public Decimal Amount { get; set; }
        public Int32 Quantity { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ShipingDate { get; set; }
        public String PointOfDelivery { get; set; }
        public String Comment { get; set; }
        public List<OrderItem> OrdersItems { get; set; }
        public Int32? ManagerId { get; set; } //

    }
}