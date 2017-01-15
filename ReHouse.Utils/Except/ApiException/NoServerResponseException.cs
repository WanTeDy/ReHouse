using System;

namespace ITfamily.Utils.Except.ApiException
{
    public class NoServerResponseException : ItFamilyException
    {
        public NoServerResponseException()
        {
        }

        public NoServerResponseException(string message) : base(message)
        {
        }

        public NoServerResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}