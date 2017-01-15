using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class BrainProductModel
    {
        public Int32 Id { get; set; }
        public String name { get; set; }
        public DateTime? DateTimeModified { get; set; }//
        public String brief_description { get; set; } //краткое описание
        public Int32 productID { get; set; }
        //public String product_code { get; set; }
        public String warranty { get; set; }
        public FromWhatProvider FromWhatProvider { get; set; }
        /// <summary>
        /// это новый товар 0(false) 1
        /// </summary>
        //public Int32 is_new { get; set; }
        /// <summary>
        /// 0(false) 1(true)
        /// </summary>
        //public Int32 is_archive { get; set; }
        //public Int32 vendorID { get; set; }
        public String articul { get; set; }
        //public Double volume { get; set; }

        

        public Int32 ItfamilyCategoryID { get; set; }
        /// <summary>
        /// USD
        /// </summary>
        public Decimal price { get; set; }
        /// <summary>
        /// UAH
        /// </summary>
        public Decimal price_uah { get; set; } //UAH
        //public Decimal priceRecomenedUAH { get; set; } //UAH
        //public List<BrainStocks> Stocks { get; set; }
        public Boolean IsAv { get; set; }
        public String IsAvailable { get; set; }
        public Boolean IsPriceForOneProduct { get; set; }
        public Decimal PriceUsdForManager { get; set; }
        public Decimal PriceUsdForPartner { get; set; }
        public Decimal PriceUsdForClients { get; set; }
        public Decimal PriceUahForClients { get; set; }
        //public Int32 BrainCategoryID { get; set; }
        //public virtual ItFamilyVendor Vendor { get; set; }
        public String VendorName { get; set; }//
        public Int32? ItfamilyVendorID { get; set; }
        public Int32 OrderForCart { get; set; }
        public String MediumImage { get; set; }
        public List<PathImages> Images { get; set; }//
    }
}