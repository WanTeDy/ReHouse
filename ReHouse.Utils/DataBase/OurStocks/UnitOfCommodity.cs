using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class UnitOfCommodity
    {
        public Int32 Id { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DateOfSale { get; set; }
        public Decimal PurchasePriceUSD { get; set; }
        public Decimal PurchasePriceUAH { get; set; }
        public Decimal SalePriceUSD { get; set; }
        public Decimal SalePriceUAH { get; set; }
        public String SerialNumber { get; set; }
        public ProductStatusInStock ProductStatusInStock { get; set; }
        [MaxLength(1000)]
        public String Notes { get; set; }
        public virtual StockProduct StockProduct { get; set; }
        public Int32 StockProductId { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 ReservedQuantity { get; set; } //TODO: Add to BASE (Than create table Reservasition with UnitOfCommodity.ID OurStockRoomId and reservequantity
        public Boolean Deleted { get; set; }
        public Int32 OurStockRoomId { get; set; }
        [NotMapped]
        public String OurStockRoomName { get; set; }
        [NotMapped]
        public String ProductStatusInStockString { get; set; }
        public virtual OurStockRoom OurStockRoom { get; set; }
        public virtual List<ReservedUnit> ReservedUnits { get; set; }

        public UnitOfCommodity()
        {
            ReservedUnits = new List<ReservedUnit>();
        }

    }
}