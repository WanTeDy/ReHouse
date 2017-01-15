using System;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class StockProductModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String BriefDescription { get; set; } //краткое описание
        public Int32 ProductId { get; set; }
        public String ProductCode { get; set; }
        public String Warranty { get; set; }
        public String Articul { get; set; }
        public Double Volume { get; set; }
        /// <summary>
        /// USD цена на момент покупки клиентом
        /// </summary>
        public Decimal PriceProviderUsd { get; set; }
        /// <summary>
        /// UAH цена на момент покупки клиентом
        /// </summary>
        public Decimal PriceProviderUah { get; set; } //UAH
        public ItFamilyVendor ItFamilyVendor { get; set; }
        public Int32 ItFamilyVendorId { get; set; }
        public ItFamilyCategory ItFamilyCategory { get; set; }
        public Int32 ItFamilyCategoryId { get; set; }
        public Int32 OurStockRoomId { get; set; }
        public Int32 Amount { get; set; }
        public String ProductStatusInStock { get; set; }
        public Int32 NeedQuantity { get; set; }
        public String Notes { get; set; }
    }
}