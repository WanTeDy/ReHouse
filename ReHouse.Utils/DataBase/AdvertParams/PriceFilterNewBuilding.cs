using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class PriceFilterNewBuilding : BaseObj
    {      
        /// <summary>
        /// min price filter in Russian
        /// </summary>
        public Int32 Min { get; set; }
        /// <summary>
        /// max price filter in Russian
        /// </summary>    
        public Int32 Max { get; set; }   
    }
}