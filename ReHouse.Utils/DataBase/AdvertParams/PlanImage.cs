using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class PlanImage : BaseObj
    {      
        /// <summary>
        /// FileName of image
        /// </summary>
        public String FileName { get; set; }
        /// <summary>
        /// Url of image
        /// </summary>
        public String Url { get; set; }

        public virtual List<Advert> Adverts { get; set; }
        public virtual List<NewBuilding> NewBuildings { get; set; }
    }
}