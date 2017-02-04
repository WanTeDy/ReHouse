using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.News;

namespace ReHouse.Utils.DataBase.Security
{
    public class Avatar : BaseObj
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
        /// User's id
        /// </summary>
        public Int32 UserId { get; set; }

        public virtual User User { get; set; }
    }
}