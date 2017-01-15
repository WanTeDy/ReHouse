using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReHouse.Utils.DataBase.Security
{
    public class Phone : BaseObj
    {
        /// <summary>
        /// User's telephone 
        /// </summary>
        public String TelePhone { get; set; }
        public virtual User User { get; set; }
    }
}
