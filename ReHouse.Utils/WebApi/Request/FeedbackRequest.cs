using System;

namespace ITfamily.Utils.WebApi.Request
{
    public class FeedbackRequest : BaseRequest
    {
        public Int32 SelId { get; set; }
        public Boolean Processed { get; set; }
        public String SendName { get; set; }
        public String Email { get; set; }
        public String Message { get; set; }
    }
}