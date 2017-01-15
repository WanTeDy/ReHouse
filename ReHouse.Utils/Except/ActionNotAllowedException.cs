using System;

namespace ReHouse.Utils.Except
{
    public class ActionNotAllowedException : ReHouseException
    {
        public ActionNotAllowedException()
        {
        }

        public ActionNotAllowedException(string message) : base(message)
        {
        }

        public ActionNotAllowedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}