using System;

namespace ITfamily.Utils.Except
{
    public class OrderException : ItFamilyException
    {
        public OrderException()
        {
        }

        public OrderException(string message) : base(message)
        {
        }

        public OrderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}