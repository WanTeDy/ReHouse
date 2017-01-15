using System;

namespace ITfamily.Utils.Except
{
    public class ExistsObjectException : ItFamilyException
    {
        public ExistsObjectException()
        {
        }

        public ExistsObjectException(string message) : base(message)
        {
        }

        public ExistsObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}