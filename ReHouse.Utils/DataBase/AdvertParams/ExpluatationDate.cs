using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class ExpluatationDate : BaseObj
    {
        /// <summary>
        /// Name of the ExpluatationDate
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Year of the ExpluatationDate
        /// </summary>
        public Int32 Year { get; set; }

        public virtual List<NewBuilding> NewBuildings { get; set; }
    }
}