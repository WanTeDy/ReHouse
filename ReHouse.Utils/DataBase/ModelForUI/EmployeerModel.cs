using System;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class EmployeerModel
    {
        public Int32 Id { get; set; }
        /// <summary>
        /// Is emploeer active
        /// </summary>
        public String IsBlocked { get; set; }

        /// <summary>
        /// Role of Empl
        /// </summary>
        public String RoleName { get; set; }

        public String FirstName { get; set; }
        public String SecondName { get; set; }
        public String FatherName { get; set; }

        public String DeliveryCity { get; set; }
        public String DeliveryStreet { get; set; }
        public String DeliveryHome { get; set; }
        public String DeliveryAppartament { get; set; }
        public String DeliveryAdditional { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Url { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        public String Login { get; set; }
    }
}