using System;
using System.ComponentModel.DataAnnotations;

namespace ITfamily.Utils.DataBase
{
    public class OrderCities
    {
        public Int32 Id { get; set; }
        [MaxLength(65)]
        public String Name { get; set; }
    }
}