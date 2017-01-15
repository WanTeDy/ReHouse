using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITfamily.Utils.DataBase.OtherOurDataForDb
{
    public class OrderItem : BaseObj
    {
        [MaxLength(150)]
        public String ProductName { get; set; }
        public Int32 productID { get; set; }
        [MaxLength(20)]
        public String product_code { get; set; }
        [MaxLength(80)]
        public String articul { get; set; }
        public Int32 quantity { get; set; }
//        public String comment { get; set; }
        //public Decimal price { get; set; }
        public Decimal SoldPrice { get; set; }
        public Decimal SoldPriceUah { get; set; }
        public Decimal PurchasePrice { get; set; }
        //public Decimal price_uah { get; set; }
        [NotMapped]
        public Int32 ProductIdForProvider { get; set; }
        [NotMapped]
        public String FromWhat { get; set; }
        [NotMapped]
        public String InStockUnitOfCommodity { get; set; }
        [NotMapped]
        public String Delivery { get; set; }
        [NotMapped]
        public DateTime? DeliveryDate { get; set; }
        [NotMapped]
        public Int32 StockQuantity { get; set; }
    }
}