using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.Geo
{
    public class City : BaseObj
    {        
        /// <summary>
        /// City's name in Russian
        /// </summary>       
        public String RussianName { get; set; }
        public virtual List<District> Districts { get; set; }
    }
}