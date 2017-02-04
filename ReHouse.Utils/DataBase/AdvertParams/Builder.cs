using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class Builder : BaseObj
    {
        /// <summary>
        /// Name of the Builder
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// Site URL
        /// </summary>
        public String Url { get; set; }

        public virtual List<NewBuilding> NewBuildings { get; set; }
    }
}