using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class PriceFilter : BaseObj
    {      
        /// <summary>
        /// Name of price filter in Russian
        /// </summary>
        public String RussianName { get; set; }        
        public virtual List<Advert> Adverts { get; set; }
    }
}