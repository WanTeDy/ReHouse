using System;
using System.Collections.Generic;

namespace ITfamily.Utils.DataBase.OtherOurDataForDb
{
    public class FrequencyPayment : BaseObj
    {
        public String Name { get; set; }
        public virtual List<Services> Serviceses { get; set; }
    }
}