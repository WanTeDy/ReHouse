using System;
using ReHouse.Utils.DataBase.AdvertParams;

namespace ReHouse.Utils.Helpers
{
    public class CartAdvertModel
    {
        public Int32 Id { get; set; }
        public Int32 Price { get; set; }
        public String Name { get; set; }
        public String Adress { get; set; }
        public String Description { get; set; }
        public Image Image { get; set; }
        public AdvertsType Type { get; set; }
        public RentPeriodType RentPeriodType { get; set; }
        public Boolean IsHot { get; set; }
        public Boolean IsExclusive { get; set; }
    }
}