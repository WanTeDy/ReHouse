using System;

namespace ITfamily.Utils.Brain
{
    public class BaseBrainResponse
    {
        public Int32 status { get; set; }
        public String result { get; set; }
        public Int32 error_code { get; set; }
        public String error_message { get; set; }
    }
}