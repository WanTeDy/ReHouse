using System;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.DataBase.PriceRules
{
    public class RuleForPrice : BaseObj
    {
        public Decimal From { get; set; }
        public Decimal To { get; set; }
        public TypeRule TypeRule { get; set; }
        public Decimal ActionRule { get; set; }
        public Role ForWhom { get; set; }
        public Int32 ForWhomId { get; set; }
        public ItFamilyCategory Category { get; set; }
        public Int32? OurCategoryId { get; set; }
    }
}