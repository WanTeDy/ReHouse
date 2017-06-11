using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.Common
{
    public class SeoParam : BaseObj
    {        
        /// <summary>
        /// Title
        /// </summary>       
        public String Title { get; set; }

        /// <summary>
        /// Description
        /// </summary>       
        public String Description { get; set; }

        /// <summary>
        /// Keywords
        /// </summary>       
        public String Keywords { get; set; }

        /// <summary>
        /// Action name
        /// </summary>       
        public String ActionName { get; set; }

        /// <summary>
        /// Controller name
        /// </summary>       
        public String ControllerName { get; set; }

        /// <summary>
        /// Url Params
        /// </summary>       
        public String UrlParams { get; set; }

        /// <summary>
        /// Full Url
        /// </summary>       
        public String FullUrl { get; set; }
    }
}