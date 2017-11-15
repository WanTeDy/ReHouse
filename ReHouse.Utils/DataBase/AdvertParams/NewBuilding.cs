using System;
using System.Linq;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Geo;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ReHouse.Utils.DataBase.AdvertParams
{
    public class NewBuilding : BaseObj
    {
        /// <summary>
        /// name of newBuilding
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// User's id, that create this advert
        /// </summary>
        public Int32 UserId { get; set; }
        /// <summary>
        /// Adress for this advert
        /// </summary>
        public String Adress { get; set; }
        /// <summary>
        /// Price for this advert
        /// </summary>
        public Int32 Price { get; set; }
        /// <summary>
        /// HouseQuantity for this advert
        /// </summary>
        public Int32 HouseQuantity { get; set; }
        /// <summary>
        /// SectionQuantity for this advert
        /// </summary>
        public Int32 SectionQuantity { get; set; }
        /// <summary>
        /// FloorQuantity for this advert
        /// </summary>
        public Int32 FloatQuantity { get; set; }
        /// <summary>
        /// FloatQuantity for this advert
        /// </summary>
        public Int32 FloorQuantity { get; set; }
        /// <summary>
        /// District's id for this advert
        /// </summary>
        public Int32 DistrictId { get; set; }
        /// <summary>
        /// ExpluatationDateId for this advert
        /// </summary>
        public Int32 ExpluatationDateId { get; set; }
        /// <summary>
        /// CreationDate for this advert
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// PublicationDate for this advert
        /// </summary>
        public DateTime? PublicationDate { get; set; }
        /// <summary>
        /// Construction of the house for this advert
        /// </summary>
        public String Construct { get; set; }
        /// <summary>
        /// WallMaterial of the house for this advert
        /// </summary>
        public String WallMaterial { get; set; }
        /// <summary>
        /// WallHeight of the house for this advert
        /// </summary>
        public Double WallHeight { get; set; }
        /// <summary>
        /// Heating of the house for this advert
        /// </summary>
        public String Heating { get; set; }
        /// <summary>
        /// Parking of the house for this advert
        /// </summary>
        public String Parking { get; set; }
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
        /// Url to site for this advert
        /// </summary>
        public String Url { get; set; }
        /// <summary>
        /// Is this advert HOT
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
        /// Description for this advert
        /// </summary>
        [AllowHtml]
        public String Description { get; set; }

        public virtual User User { get; set; }
        public virtual District District { get; set; }
        public virtual ExpluatationDate ExpluatationDate { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<PlanImage> PlanImages { get; set; }
        public virtual List<Builder> Builders { get; set; }
        [NotMapped]
        public virtual List<int> BuildersId { get; set; }
        public virtual List<Phone> Phones { get; set; }
    }
}