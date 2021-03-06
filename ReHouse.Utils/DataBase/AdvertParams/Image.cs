﻿using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.News;
using ReHouse.Utils.DataBase.Feedback;

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
        /// <summary>
        /// Alt param of image
        /// </summary>
        public String Alt { get; set; }
        /// <summary>
        /// Title param of image
        /// </summary>
        public String Title { get; set; }

        public virtual List<Advert> Adverts { get; set; }
        public virtual List<NewBuilding> NewBuildings { get; set; }
        public virtual List<Article> Articles { get; set; }
    }
}