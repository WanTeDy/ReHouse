using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.Geo
{
    public class District : BaseObj
    {
        /// <summary>
        /// District's name in Russian
        /// </summary>       
        public String RussianName { get; set; }
        /// <summary>
        /// City's id of this district
        /// </summary>       
        public Int32 CityId { get; set; }
        public virtual City City { get; set; }
        public virtual List<Advert> Adverts { get; set; }
    }
}