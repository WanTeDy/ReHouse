using System;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class AdditionalDataForAnalyzeGoods
    {
        public OrderType OrderType { get; set; }
        public String OrderTypeString { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public String PaymentStatusString { get; set; }
        public String FIO { get; set; }
        public String Phone { get; set; }
        public Decimal SoldPrice { get; set; }
        //public Decimal SoldPriceUah { get; set; }
        public Decimal PurchasePrice { get; set; }
        public Int32 Quantity { get; set; } 
    }
}