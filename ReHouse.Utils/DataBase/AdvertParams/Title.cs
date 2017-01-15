using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class Title : BaseObj
    {
        /// <summary>
        /// Name of advert title in Russia
        /// </summary>
        public String RussianName { get; set; }
        public virtual List<Advert> Adverts { get; set; }
    }
}