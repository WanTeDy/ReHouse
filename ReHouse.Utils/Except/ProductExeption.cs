using System;

namespace ITfamily.Utils.Except
{
    public class ProductExeption : ItFamilyException
    {
        public ProductExeption()
        {
        }

        public ProductExeption(string message) : base(message)
        {
        }

        public ProductExeption(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}