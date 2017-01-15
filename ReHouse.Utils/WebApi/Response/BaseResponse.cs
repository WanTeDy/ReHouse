using System;
using ITfamily.Utils.Except;

namespace ITfamily.Utils.WebApi.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            ErrorCode = (Int32) ErrorCodes.Success;
        }
        public Int32 ErrorCode { get; set; }
        public String ExceptionMessage { get; set; }
        public String StackTrace { get; set; }
        public String ExceptionName { get; set; } 
    }
}