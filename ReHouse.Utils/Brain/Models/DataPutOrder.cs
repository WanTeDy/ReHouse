using System;
using ITfamily.Utils.Brain.Request;

namespace ITfamily.Utils.Brain.Models
{
    public class DataPutOrder:BaseBrainRequest
    {
        /// <summary>
        /// Обязательный параметр - да;	валюта (USD, UAH)
        /// </summary>
        public String currency { get; set; }
        /// <summary>
        /// Обязательный параметр - да;	идентификатор пункта выдачи / службы доставки
        /// </summary>
        public Int32 targetID { get; set; }
        /// <summary>
        /// Обязательный параметр - нет;	идентификатор адреса доставки
        /// </summary>
        public Int32? addressID { get; set; }
        /// <summary>
        /// Обязательный параметр - нет;	идентификатор контактного лица
        /// </summary>
        public Int32? contactID { get; set; }
        /// <summary>
        /// Обязательный параметр - нет;	идентификатор клиента
        /// </summary>
        public Int32? clientID { get; set; }
        /// <summary>
        /// Обязательный параметр - нет;	комментарий к заказу
        /// </summary>
        public String comment { get; set; }
    }
}