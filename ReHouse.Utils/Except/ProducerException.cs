using System;

namespace ITfamily.Utils.Except
{
    public class ProducerException : ItFamilyException
    {
        public ProducerException()
        {
        }

        public ProducerException(string message) : base(message)
        {
        }

        public ProducerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}