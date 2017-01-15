using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class AdvertPropertyValue : BaseObj
    {        
        /// <summary>
        /// Property value's name in Russian
        /// </summary>     
        public String RussianName { get; set; }
        /// <summary>
        /// Advert property's id
        /// </summary>     
        public Int32 AdvertPropertyId { get; set; }

        public virtual AdvertProperty AdvertProperty { get; set; } 
    }
}