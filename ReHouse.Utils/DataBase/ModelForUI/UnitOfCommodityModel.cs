using System;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class UnitOfCommodityModel
    {
        public Int32 Id { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DateOfSale { get; set; }
        public Decimal PurchasePriceUSD { get; set; }
        public Decimal PurchasePriceUAH { get; set; }
        public Decimal SalePriceUSD { get; set; }
        public Decimal SalePriceUAH { get; set; }
        public String SerialNumber { get; set; }
        public String ProductStatusInStockName { get; set; }
        public String Notes { get; set; }
        //public virtual StockProduct StockProduct { get; set; }
        public Int32 StockProductId { get; set; }
        public Int32 OurStockRoomId { get; set; }
        public String OurStockRoomName { get; set; }
        //public virtual OurStockRoom OurStockRoom { get; set; } 
    }
}