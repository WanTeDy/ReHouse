﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace ReHouse.Utils.DataBase.Common
{
    public class SliderParam : BaseObj
    {        
        /// <summary>
        /// Url
        /// </summary>       
        public String Url { get; set; }

        /// <summary>
        /// Url
        /// </summary>       
        public String VideoUrl { get; set; }

        /// <summary>
        /// Url
        /// </summary>       
        public String ButtonUrl { get; set; }

        /// <summary>
        /// is this url video
        /// </summary>       
        public bool IsVideo { get; set; }

        /// <summary>
        /// Text
        /// </summary>   
        [AllowHtml]
        public String Text { get; set; }

        [NotMapped]  
        public HttpPostedFileBase Image { get; set; }
    }
}