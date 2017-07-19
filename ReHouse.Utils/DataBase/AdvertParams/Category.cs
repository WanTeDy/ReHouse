using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class Category : BaseObj
    {
        /// <summary>
        /// Parent's category id if present
        /// </summary>
        public Int32? ParentId { get; set; }
        /// <summary>
        /// Category's name in Russian
        /// </summary>     
        public String RussianName { get; set; }        

        public virtual Category Parent { get; set; }
        public virtual List<TagPage> TagPages { get; set; }
        public virtual List<Advert> Adverts { get; set; }        
        public virtual List<AdvertProperty> AdvertProperties { get; set; }
    }
}