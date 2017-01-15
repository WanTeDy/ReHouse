using System;
using System.ComponentModel.DataAnnotations;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase.Currencies
{
    public class Currency : BaseObj
    {
        public Decimal Value { get; set; }
        [MaxLength(40)]
        public String Name { get; set; }
        [MaxLength(50)]
        public String BelongsCourse { get; set; }
        [MaxLength(50)]
        public String BelongsCourseType { get; set; }
        public DateTime DateTime { get; set; }
        public BelongsType EnumBelongsType { get; set; }
        public Int32? CurrencyId { get; set; }
        //public CollectionCurrency CollectionCurrency { get; set; }
        //public Int32 CollectionCurrencyId { get; set; }
    }
}