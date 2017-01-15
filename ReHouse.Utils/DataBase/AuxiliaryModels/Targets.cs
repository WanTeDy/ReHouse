using System;
using System.Collections.Generic;

namespace ITfamily.Utils.DataBase.AuxiliaryModels
{
    public class Targets
    {
        public Int32 Id { get; set; }
        public Int32 targetID { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public String region { get; set; }
        public List<Addresses> Addresses { get; set; }

        public Targets()
        {
            Addresses = new List<Addresses>();
        }
    }
}