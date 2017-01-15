using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.DataBase
{
    public class BrainProduct : BaseObj
    {
        [MaxLength(255)]
        public String name { get; set; }
        [MaxLength(600)]
        public String brief_description { get; set; } //краткое описание
        [MaxLength(8000)]
        public String description { get; set; }
        public Int32 productID { get; set; }
        [MaxLength(20)]
        public String product_code { get; set; }
        [MaxLength(13)]
        public String warranty { get; set; }
        /// <summary>
        /// 0(false) 1(true)
        /// </summary>
        public Int32 is_archive { get; set; }
        public Int32 vendorID { get; set; }
        [MaxLength(80)]
        public String articul { get; set; }
        public Double volume { get; set; }
        /// <summary>
        /// это новый товар 0(false) 1
        /// </summary>
        public Int32 is_new { get; set; }
        public Int32 categoryID { get; set; }
        /// <summary>
        /// USD
        /// </summary>
        public Decimal price { get; set; }
        /// <summary>
        /// UAH
        /// </summary>
        public Decimal price_uah { get; set; } //UAH
        public Decimal recommendable_price { get; set; } //UAH
        public List<Int32> stocks { get; set; }

        public DateTime? DateTimeModified { get; set; }
        [MaxLength(255)]
        public String MainImage { get; set; }
        public virtual List<PathImages> PathImageses { get; set; }
        //public String small_image { get; set; }//URL to small image
        //public String medium_image { get; set; }//URL to medium image
        //public String large_image { get; set; }//URL to large image
        
        public virtual BrainCategory BrainCategory { get; set; }
        public Int32 BrainCategoryID { get; set; }
        public virtual Vendor Vendor { get; set; }
        public Int32? BrainVendorID { get; set; }
        public virtual List<BrainStocks> BrainStockses { get; set; }

        public BrainProduct()
        {
            //BrainStockses = new List<BrainStocks>();
        }

        //public DateTime date_added { get; set; } //убрали
        //public DateTime date_modified { get; set; } //убрали
        //public Boolean free_shipping { get; set; }//бесплатная доставка //убрали
        //public Int32 min_order_amount { get; set; } //минимальное количество заказа //убрали
        //public Boolean shipping_freight { get; set; } //доставка груза //убрали
        //public Int32 actionID { get; set; } //убрали
        //public Boolean is_price_cut { get; set; }//это сниженная цена true false //убрали
        //public Boolean self_delivery { get; set; } //самовывоз //убрали
        public virtual List<Specifications> options { get; set; }//перечень характеристик
    }
}