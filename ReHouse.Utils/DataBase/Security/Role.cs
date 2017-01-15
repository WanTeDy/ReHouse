using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReHouse.Utils.DataBase.Security
{
    public class Role : BaseObj
    {        
        /// <summary>
        /// Name of Role in Russian
        /// </summary>
        [MaxLength(30)]
        public String RussianName { get; set; }

        public virtual List<Authority> Authorities { get; set; }
        public virtual List<User> Users { get; set; }
    }
}