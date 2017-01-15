using System;

namespace ITfamily.Utils.DataBase.Security
{
    public class Employeer : BaseObj
    {
        /// <summary>
        /// Is emploeer active
        /// </summary>
        public Boolean IsActive { get; set; }

        /// <summary>
        /// Role of Empl
        /// </summary>
        public Role Role { get; set; }

        public Int32 RoleId { get; set; }
        public String FirstName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public String SecondName { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public String FatherName { get; set; }

        public String Adress { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Url { get; set; }

        public String Login { get; set; }
        public String Password { get; set; }

        public String ProviderLogin1 { get; set; }
        public String ProviderPassword1 { get; set; }
        public String TokenHash { get; set; }
    }
}