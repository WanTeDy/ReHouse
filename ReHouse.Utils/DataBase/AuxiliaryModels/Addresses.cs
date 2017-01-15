using System;
using System.Collections.Generic;

namespace ITfamily.Utils.DataBase.AuxiliaryModels
{
    public class Addresses
    {
        public Int32 Id { get; set; }
        public Int32 addressID { get; set; }
        public String name { get; set; }
        public String address { get; set; }
        public String targets { get; set; }
    }
}