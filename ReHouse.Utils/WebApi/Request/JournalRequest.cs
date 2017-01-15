using System;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.WebApi.Request
{
    public class JournalRequest : BaseRequest
    {
        public OrderType OrderType { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}