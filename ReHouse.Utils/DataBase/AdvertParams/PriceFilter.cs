using System;
using ReHouse.Utils.Helpers;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class PriceFilter : BaseObj
    {
        /// <summary>
        /// min price filter in Russian
        /// </summary>
        public Int32 Min { get; set; }
        /// <summary>
        /// max price filter in Russian
        /// </summary>    
        public Int32 Max { get; set; }
        /// <summary>
        /// max price filter in Russian
        /// </summary>    
        public AdvertsType AdvertType { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public String Currency { get; set; }

    }
}