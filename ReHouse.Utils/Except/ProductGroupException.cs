using System;

namespace ITfamily.Utils.Except
{
    public class ProductGroupException : ItFamilyException
    {
        public ProductGroupException()
        {
        }

        public ProductGroupException(string message) : base(message)
        {
        }

        public ProductGroupException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}