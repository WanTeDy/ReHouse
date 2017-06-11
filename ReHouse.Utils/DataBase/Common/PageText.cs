using System;
using System.Collections.Generic;

namespace ReHouse.Utils.DataBase.Common
{
    public class PageText : BaseObj
    {        
        /// <summary>
        /// Action name
        /// </summary>       
        public String ActionName { get; set; }

        /// <summary>
        /// Controller name
        /// </summary>       
        public String ControllerName { get; set; }

        /// <summary>
        /// Text Block name
        /// </summary>       
        public String TextBlockName { get; set; }

        /// <summary>
        /// Description
        /// </summary>       
        public String Description { get; set; }

        /// <summary>
        /// Title
        /// </summary>       
        public String Title { get; set; }
    }
}