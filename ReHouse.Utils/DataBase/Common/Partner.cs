using System;
using System.Web.Mvc;
using System.Collections.Generic;
using ReHouse.Utils.DataBase.Security;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.DataBase.News
{
    public class Partner : BaseObj
    {       
        public DateTime CreationDate { get; set; }
        public virtual Image Image { get; set; }
    }
}