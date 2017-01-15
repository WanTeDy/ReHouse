using System;

namespace ITfamily.Utils.WebApi.Request
{
    public class ItfamilyCategoryRequest : BaseRequest
    {
        public Boolean IsSite { get; set; }
        public Boolean IsProvider { get; set; }
    }
}