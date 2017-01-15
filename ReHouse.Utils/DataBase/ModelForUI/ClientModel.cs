using System;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class ClientModel
    {
        public Int32 Id { get; set; }

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
        //public String Login { get; set; }
        public Int32 RoleId { get; set; }
        public String Password { get; set; }
        public Boolean IsActive { get; set; }
    }
}