using System;

namespace ITfamily.Utils.Brain.Request
{
    public class OrderShipRequest : BaseBrainRequest
    {
        /// <summary>
        /// Обязательный параметр - да;
        /// дата и время отгрузки
        /// Используемый формат даты - DD.MM.YYYY HH:ii 
        /// </summary>
        public String shipingdate { get; set; }
        /// <summary>
        /// Обязательный параметр - нет; идентификатор филиала компании
        /// </summary>
        //public Int32 subsidiaryID { get; set; }
        /// <summary>
        /// Обязательный параметр - нет; необходимость бухучета (1-да, 0-нет)
        /// </summary>
        public Int32 accounting { get; set; }
        /// <summary>
        /// Обязательный параметр - нет; идентификатор клиента
        /// </summary>
        //public Int32 clientID { get; set; }
    }
}