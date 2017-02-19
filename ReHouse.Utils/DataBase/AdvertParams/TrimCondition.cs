using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReHouse.Utils.DataBase;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class TrimCondition : BaseObj
    {
        /// <summary>
        /// Russian Name for trim condition
        /// </summary>
        public String RussianName { get; set; }

        public virtual List<Advert> Adverts { get; set; }
    }
}
