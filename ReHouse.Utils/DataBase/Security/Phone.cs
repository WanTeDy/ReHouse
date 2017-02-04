using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.Security
{
    public class Phone : BaseObj
    {
        /// <summary>
        /// User's telephone 
        /// </summary>
        public String TelePhone { get; set; }
        public virtual User User { get; set; }
        public virtual Builder Builder { get; set; }
    }
}
