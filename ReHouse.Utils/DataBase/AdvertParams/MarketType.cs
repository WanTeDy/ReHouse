using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class MarketType : BaseObj
    {
        /// <summary>
        /// Name of advert MarketType in Russia
        /// </summary>
        public String RussianName { get; set; }

        public virtual List<Advert> Adverts { get; set; }
    }
}