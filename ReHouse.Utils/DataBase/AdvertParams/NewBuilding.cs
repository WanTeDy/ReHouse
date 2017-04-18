using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.Geo;

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
        /// PublicationDate for this advert
        /// </summary>
        public DateTime PublicationDate { get; set; }
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
        /// Url to site for this advert
        /// </summary>
        public String Url { get; set; }

        public virtual User User { get; set; }
        public virtual District District { get; set; }
        public virtual ExpluatationDate ExpluatationDate { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<PlanImage> PlanImages { get; set; }
        public virtual List<Builder> Builders { get; set; }
        public virtual List<Phone> Phones { get; set; }
    }
}