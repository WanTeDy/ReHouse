using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.DataBase.OtherOurDataForDb
{
    public class OrderComes : BaseObj
    {
        public OrderType OrderType { get; set; }
        
        //TODO добавить в бизнесс операцию проверку актуальности наличия на удаленном складе
        public Boolean IsExistsInOtherStock { get; set; }
        
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public CurrencyType Currency { get; set; }
        [MaxLength(128)]
        public String FIO { get; set; }
        [MaxLength(20)]
        public String Phone { get; set; }
        [MaxLength(255)]
        public String Email { get; set; }
        [MaxLength(255)]
        public String Adress { get; set; }
        public Decimal Amount { get; set; }
        public DateTime? DeliveryDate { get; set; }//время прибытия товара
        public DateTime? ShipingDate { get; set; }
        [MaxLength(300)]
        public String PointOfDelivery { get; set; }
        //public Int32? ClientId { get; set; }
        //public Int32? EntrepreneurId { get; set; }
        public Int32 ContractorId { get; set; }
        [MaxLength(600)]
        public String Comment { get; set; }
        public Int32 Quantity { get; set; }
        [MaxLength(500)]
        public String Notes { get; set; }
        public virtual List<OrderItem> OrdersItems { get; set; }
        public virtual CustomerCard CustomerCard { get; set; }
        public Int32? ManagerId { get; set; }
        public virtual List<ReservedUnit> ReservedUnits { get; set; }


        public OrderComes()
        {
            OrdersItems = new List<OrderItem>();
            ReservedUnits = new List<ReservedUnit>();
            IsExistsInOtherStock = false;
        }
    }
}