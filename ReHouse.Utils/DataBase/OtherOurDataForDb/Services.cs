using System;

namespace ITfamily.Utils.DataBase.OtherOurDataForDb
{
    public class Services : BaseObj
    {
        public String Name { get; set; }
        public virtual FrequencyPayment FrequencyPayment { get; set; }
        public Int32 FrequencyPaymentId { get; set; }
        public Decimal PriceMin { get; set; }
        public Decimal PriceRec { get; set; }
        public Decimal PriceRetail { get; set; }
    }
}