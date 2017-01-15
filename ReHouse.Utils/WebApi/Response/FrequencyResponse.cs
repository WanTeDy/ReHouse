using System.Collections.Generic;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.WebApi.Response
{
    public class FrequencyResponse : BaseResponse
    {
        public List<FrequencyPayment> FrequencyPayments { get; set; }
        public FrequencyPayment FrequencyPayment { get; set; }
    }
}