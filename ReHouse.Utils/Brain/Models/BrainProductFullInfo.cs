using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.ModelForUI;

namespace ITfamily.Utils.Brain.Models
{
    public class BrainProductFullInfo
    {
        public Int32 Id { get; set; }
        public String name { get; set; }
        public String brief_description { get; set; } //краткое описание
        public String description { get; set; }
        public Int32 productID { get; set; }
        public String product_code { get; set; }
        public String warranty { get; set; }
        public Boolean is_archive { get; set; } // 0(false) 1(true)
        public Int32 vendorID { get; set; }
        public String articul { get; set; }
        public Double volume { get; set; }
        public Boolean is_new { get; set; } //это новый товар 0(false) 1
        public Int32 categoryID { get; set; }
        public Decimal price { get; set; } //USD
        public Decimal price_uah { get; set; } //UAH
        public String small_image { get; set; }//URL to small image
        public String medium_image { get; set; }//URL to medium image
        public String large_image { get; set; }//URL to large image

        //public List<Int32> stocks { get; set; }

        public String date_added { get; set; }
        public String date_modified { get; set; }
        public Int32 free_shipping { get; set; }//бесплатная доставка
        public Int32 min_order_amount { get; set; } //минимальное количество заказа
        public Int32 shipping_freight { get; set; } //доставка груза
        public Int32 actionID { get; set; } //без понятия что за поле api.brain.com.ua 
        public Boolean is_price_cut { get; set; }//это сниженная цена true false
        public String self_delivery { get; set; } //самовывоз
        public List<Specifications> options { get; set; }//перечень характеристик

        //все то что выше это с api, если потребуется можно добавить ourPrice
        public Decimal recommendable_price { get; set; }//розничная цена 
    }
}