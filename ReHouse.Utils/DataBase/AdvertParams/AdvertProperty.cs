using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class AdvertProperty : BaseObj
    {        
        /// <summary>
        /// Property's name in Russian
        /// </summary>     
        public String RussianName { get; set; }        
                
        public virtual List<Category> Categories { get; set; }        
        public virtual List<AdvertPropertyValue> AdvertPropertyValues { get; set; }
    }
}