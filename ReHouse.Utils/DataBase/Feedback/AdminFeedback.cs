using System;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.Feedback
{
    public class AdminFeedback : BaseObj
    {
        /// <summary>
        /// Feedback's user name
        /// </summary>       
        public String Username { get; set; }
        /// <summary>
        /// Feedback's ImageUrl
        /// </summary>       
        public String Adress { get; set; }
        /// <summary>
        /// Feedback's description
        /// </summary> 
        public String Description { get; set; }
        /// <summary>
        /// Feedback's creation datetime
        /// </summary> 
        public DateTime CreationDate { get; set; }

        public virtual Image Image { get; set; }
    }
}