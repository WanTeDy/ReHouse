using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.Geo
{
    public class District : BaseObj
    {
        /// <summary>
        /// Parrent's id
        /// </summary> 
        public Int32? ParrentId { get; set; }
        /// <summary>
        /// District's name in Russian
        /// </summary>       
        public String RussianName { get; set; }

        public virtual District Parrent { get; set; }
        public virtual List<TagPage> TagPages { get; set; }
        public virtual List<Advert> Adverts { get; set; }
        public virtual List<NewBuilding> NewBuildings { get; set; }
    }
}