using System;

namespace ITfamily.Utils.WebApi.Request
{
    public class RemindRequest : BaseRequest
    {
        public String Password { get; set; }
        public String Email { get; set; }
    }
}