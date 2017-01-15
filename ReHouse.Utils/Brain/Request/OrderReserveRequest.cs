using System;


namespace ITfamily.Utils.Brain.Request
{
    public class OrderReserveRequest : BaseBrainRequest
    {
        /// <summary>
        /// Обязательный параметр - нет;
        /// дата бронирования (по умолчанию - +1 сутки к текущей дате)
        /// Используемый формат даты - DD.MM.YYYY 
        /// </summary>
        //public String reserveddate { get; set; }

        public String login { get; set; }
        public String password { get; set; }


        /// <summary>
        /// Обязательный параметр - нет; идентификатор клиента
        /// </summary>
        //public Int32 clientID { get; set; }
    }
}