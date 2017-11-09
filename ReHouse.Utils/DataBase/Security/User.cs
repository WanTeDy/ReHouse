using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.Security
{
    public class User : BaseObj
    {
        /// <summary>
        /// FirstName
        /// </summary>
        public String FirstName { get; set; }
        /// <summary>
        /// SecondName
        /// </summary>
        public String SecondName { get; set; }
        /// <summary>
        /// FatherName
        /// </summary>
        public String FatherName { get; set; }
        /// <summary>
        /// MessengerName
        /// </summary>
        public String MessengerName { get; set; }
        /// <summary>
        /// User's Position
        /// </summary>
        public String Position { get; set; }
        /// <summary>
        /// FatherName
        /// </summary>
        public String Adress { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        public String Login { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public String Password { get; set; }
        /// <summary>
        /// Id user's Role
        /// </summary>
        public Int32 RoleId { get; set; }
        /// <summary>
        /// Id user's avatar
        /// </summary>
        public Int32 AvatarId { get; set; }
        /// <summary>
        /// TokenHash
        /// </summary>
        public String TokenHash { get; set; }
        /// <summary>
        /// About user
        /// </summary>
        public String About { get; set; }
        /// <summary>
        /// User is Active
        /// </summary>
        public Boolean IsActive { get; set; }

        /// <summary>
        /// Ordering in list
        /// </summary>
        public Int32 OrderByField { get; set; }

        public virtual Role Role { get; set; }
        public virtual Avatar Avatar { get; set; }
        public virtual List<Phone> Phones { get; set; }        
        public virtual List<NewBuilding> NewBuildings { get; set; }        
        public virtual List<Advert> Adverts { get; set; }
    }
}