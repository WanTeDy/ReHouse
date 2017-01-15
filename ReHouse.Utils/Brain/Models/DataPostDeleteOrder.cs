using System;

namespace ITfamily.Utils.Brain.Models
{
    public class DataPostDeleteOrder
    {
        /// <summary>
        /// Обязательный параметр - нет (если указан product_code);	
        /// ID товара, который нужно удалить из заказа
        /// </summary>
        public Int32 productID { get; set; }
        /// <summary>
        /// Обязательный параметр - нет (если указан productID);
        /// код товара, который нужно удалить из заказа
        /// </summary>
        public String product_code { get; set; }
    }
}