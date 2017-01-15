using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Geo;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class Advert : BaseObj
    {
        /// <summary>
        /// User's id, that create this advert
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        /// Category's id for this advert
        /// </summary>
        public Int32 CategoryId { get; set; }
        /// <summary>
        /// Title's id for this advert
        /// </summary>
        public Int32 TitleId { get; set; }
        /// <summary>
        /// District's id for this advert
        /// </summary>
        public Int32 DistrictId { get; set; }
        /// <summary>
        /// Street for this advert
        /// </summary>
        public String Street { get; set; }
        /// <summary>
        /// Price for this advert
        /// </summary>
        public Int32 Price { get; set; }
        /// <summary>
        /// PriceFilter's id for this advert
        /// </summary>
        public Int32 PriceFilterId { get; set; }
        /// <summary>
        /// Discription for this advert
        /// </summary>
        public String Description { get; set; }
        /// <summary>
        /// Expire date for this advert
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// Url to YouTube for this advert
        /// </summary>
        public String YouTubeUrl { get; set; }
        /// <summary>
        /// Is this advert marked "HOT"
        /// </summary>
        public Boolean IsHot { get; set; }

        public virtual Category Category { get; set; }
        public virtual PriceFilter PriceFilter { get; set; }
        public virtual Title Title { get; set; }
        public virtual District District { get; set; }
        public virtual User User { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}