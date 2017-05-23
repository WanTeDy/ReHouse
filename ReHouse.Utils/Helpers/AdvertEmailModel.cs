using System;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.Helpers
{
    public class AdvertEmailModel : CommonEmailModel
    {
        public Int32 Id { get; set; }
        public String Adress { get; set; }
        public String Title { get; set; }
        public String Url { get; set; }
        public AdvertsType Type { get; set; }
        public String Price { get; set; }        
    }
}