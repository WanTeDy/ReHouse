using System;

namespace ITfamily.Utils.Brain.Models
{
    public class DataPostOrder
    {
        /// <summary>
        /// не обязательный параметр(если указан product_code или articul)	
        /// ID товара, который нужно добавить в заказ
        /// </summary>
        public Int32 productID { get; set; }
        /// <summary>
        /// не обязательный параметр(если указан productID или articul)	
        /// код товара, который нужно добавить в заказ
        /// </summary>
        public String product_code { get; set; }
        /// <summary>
        /// не обязательный параметр(если указан productID или product_code)	
        /// артикул товара, который нужно добавить в заказ
        /// </summary>
        public String articul { get; set; }
        /// <summary>
        /// обязательный параметр; 
        /// Количество единиц товара
        /// </summary>
        public Int32 quantity { get; set; }
        /// <summary>
        /// не обязательный параметр;
        /// Комментарий к товару
        /// </summary>
        public String comment { get; set; }
    }
}