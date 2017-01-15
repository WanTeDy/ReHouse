using System;

namespace ITfamily.Utils.WebApi.Request
{
    public class FrequencyRequest : BaseRequest
    {
        public Int32 SelId { get; set; }
        public String Name { get; set; } 
    }
}