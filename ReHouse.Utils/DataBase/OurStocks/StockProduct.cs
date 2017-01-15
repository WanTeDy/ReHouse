using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Filters;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class StockProduct : BaseObj
    {
        public FromWhatProvider FromWhatProvider { get; set; }
        [MaxLength(255)]
        public String Name { get; set; }
        [MaxLength(600)]
        public String BriefDescription { get; set; } //краткое описание
        public Int32 ProductId { get; set; }
        [MaxLength(13)]
        public String Warranty { get; set; }
        [MaxLength(80)]
        public String Articul { get; set; }
        public Decimal Price { get; set; }
        public Decimal PriceUah { get; set; }
        public Boolean IsPriceForOneProduct { get; set; }
        public Decimal PriceUsdForManager { get; set; }
        public Decimal PriceUsdForPartner { get; set; }
        public Decimal PriceUsdForClients { get; set; }
        [NotMapped]
        public Decimal PriceUahForClients { get; set; }
        public Boolean IsAvailable { get; set; }
        public Int32 CounterVisit { get; set; }
        [MaxLength(255)]
        public String MainImage { get; set; }
        public virtual List<ProductPropertyValues> PropertyValueses { get; set; }
        public virtual AdditionalStockProductData AdditionalData { get; set; }


        //public Decimal PriceProviderUsd { get; set; }
        //public Decimal PriceProviderUah { get; set; } //UAH

        public virtual ItFamilyCategory ItFamilyCategory { get; set; }
        public Int32 ItFamilyCategoryId { get; set; }
        public virtual ItFamilyVendor ItFamilyVendor { get; set; }
        public Int32 ItFamilyVendorId { get; set; }
        //public Int32 Amount { get; set; }
        
        //public Int32 OurStockRoomId { get; set; }
        [MaxLength(1000)]
        public String Notes { get; set; }
        [NotMapped]
        public String Available { get; set; }
        [NotMapped]
        public String FromWhatProviderString { get; set; }
        public virtual List<UnitOfCommodity> UnitOfCommodities { get; set; }
    }
}