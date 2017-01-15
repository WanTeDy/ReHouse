using System;

namespace ITfamily.Utils.WebApi.Request.Brain
{
    public class ImportRequest : BaseRequest
    {
        public Int32 Sleep { get; set; }
        public DateTime DateTime { get; set; }
    }
}