using System;

namespace ITfamily.Utils.Except
{
    public class StockRoomException : ItFamilyException
    {
        public StockRoomException()
        {
        }

        public StockRoomException(string message) : base(message)
        {
        }

        public StockRoomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}