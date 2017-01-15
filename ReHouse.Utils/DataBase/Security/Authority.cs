using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.Security
{
    public class Authority : BaseObj
    {        
        /// <summary>
        /// Name of user's authority operation
        /// </summary>
        public String NameBusinessOperation { get; set; }
        /// <summary>
        /// Name of user's authority in Russian
        /// </summary>
        public String RussianNameOperation { get; set; }
        public virtual List<Role> Roles { get; set; }
    }
}