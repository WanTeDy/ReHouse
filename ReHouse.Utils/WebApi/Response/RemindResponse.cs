using System;

namespace ITfamily.Utils.WebApi.Response
{
    public class RemindResponse : BaseResponse
    {
        public Boolean Access { get; set; }
        public String TokenHash { get; set; }
        public String NameContractor { get; set; }
        public String SecondNameContractor { get; set; }
        public String FatherNameContractor { get; set; }
        public String Email { get; set; }
    }
}