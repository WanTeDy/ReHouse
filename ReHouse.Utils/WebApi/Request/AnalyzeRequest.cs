using System;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.WebApi.Request
{
    public class AnalyzeRequest : BaseRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Decimal FromPrice { get; set; }
        public Decimal ToPrice { get; set; }
        public Int32 CategoryId { get; set; }
        public FromWhatProvider FromWhat { get; set; }
    }
}