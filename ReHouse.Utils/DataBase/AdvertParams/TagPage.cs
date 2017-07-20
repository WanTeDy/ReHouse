using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class TagPage : BaseObj
    {
        /// <summary>
        /// Property's name in Russian
        /// </summary>     
        public String RussianName { get; set; }

        /// <summary>
        /// Property's name in Russian
        /// </summary>     
        public String ShortName { get; set; }
        /// <summary>
        /// type
        /// </summary>     
        public TagPageType TagPageType { get; set; }
        /// <summary>
        /// type
        /// </summary>     
        public AdvertsType AdvertsType { get; set; }

        /// <summary>
        /// Description
        /// </summary>   
        [AllowHtml]
        public String SeoText { get; set; }

        [NotMapped]
        public Int32 Quantity { get; set; }

        public virtual District District { get; set; }
        public virtual Category Category { get; set; }
    }
}