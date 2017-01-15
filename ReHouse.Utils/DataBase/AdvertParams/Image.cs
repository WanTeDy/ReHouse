using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class Image : BaseObj
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
    }
}