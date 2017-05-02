using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.News
{
    public class Article : BaseObj
    {        
        /// <summary>
        /// Article's title
        /// </summary>       
        public String Title { get; set; }
        /// <summary>
        /// Article's description
        /// </summary> 
        public String Description { get; set; }
        /// <summary>
        /// Article's userId
        /// </summary> 
        public Int32 UserId { get; set; }        
        /// <summary>
        /// Article's creation datetime
        /// </summary> 
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual List<Image> Images { get; set; }
    }
}