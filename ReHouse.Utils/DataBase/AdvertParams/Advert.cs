using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Geo;
using ReHouse.Utils.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

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
        /// TrimCondition's id for this advert
        /// </summary>
        public Int32? TrimConditionId { get; set; }
        /// <summary>
        /// MarketType's id for this advert
        /// </summary>
        public Int32 MarketTypeId { get; set; }
        /// <summary>
        /// Street for this advert
        /// </summary>
        public String Street { get; set; }
        /// <summary>
        /// Title for this advert
        /// </summary>
        public String TitleName { get; set; }
        /// <summary>
        /// Price for this advert
        /// </summary>
        public Int32 Price { get; set; }
        /// <summary>
        /// Discription for this advert
        /// </summary>
        [AllowHtml]
        public String Description { get; set; }
        /// <summary>
        /// Expire date for this advert
        /// </summary>
        public DateTime ExpireDate { get; set; }
        /// <summary>
        /// CreationDate for this advert
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// PublicationDate for this advert
        /// </summary>
        public DateTime? PublicationDate { get; set; }
        /// <summary>
        /// Url to YouTube for this advert
        /// </summary>
        public String YouTubeUrl { get; set; }
        /// <summary>
        /// Latitude for this advert
        /// </summary>
        public Double Latitude { get; set; }
        /// <summary>
        /// Longitude for this advert
        /// </summary>
        public Double Longitude { get; set; }
        /// <summary>
        /// Is this advert marked "HOT"
        /// </summary>
        public Boolean IsHot { get; set; }
        /// <summary>
        /// Is this advert Exclusive
        /// </summary>
        public Boolean IsExclusive { get; set; }
        /// <summary>
        /// Is this advert new
        /// </summary>
        [NotMapped]
        public Boolean IsNew
        {
            get
            {
                var date = DateTime.Now.AddDays(-7);
                return this.PublicationDate > date;
            }
        }
        /// <summary>
        /// Is this advert moderated by admin
        /// </summary>
        public Boolean IsModerated { get; set; }
        /// <summary>
        /// Type of this advert
        /// </summary>
        public AdvertsType Type { get; set; }
        /// <summary>
        /// Type of this advert
        /// </summary>
        public RentPeriodType RentPeriodType { get; set; }

        public virtual Category Category { get; set; }
        public virtual MarketType MarketType { get; set; }
        public virtual Title Title { get; set; }
        public virtual District District { get; set; }
        public virtual TrimCondition TrimCondition { get; set; }
        public virtual User User { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<PlanImage> PlanImages { get; set; }
        public virtual List<AdvertPropertyValue> AdvertPropertyValues { get; set; }
    }
}