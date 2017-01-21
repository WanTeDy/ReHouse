using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class AdvertPropertyValue : BaseObj
    {        
        /// <summary>
        /// Properties value
        /// </summary>     
        public String PropertiesValue { get; set; }
        /// <summary>
        /// Advert property's id
        /// </summary>     
        public Int32 AdvertPropertyId { get; set; }
        /// <summary>
        /// Advert's id
        /// </summary>     
        public Int32 AdvertId { get; set; }

        public virtual AdvertProperty AdvertProperty { get; set; }
        public virtual Advert Advert { get; set; }         
    }
}