using System;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.DataBase.PriceRules;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class RuleForPriceModel
    {
        public Int32 Id { get; set; }
        public Boolean Deleted { get; set; }
        public Decimal From { get; set; }
        public Decimal To { get; set; }
        public String TypeRule { get; set; }
        public Decimal ActionRule { get; set; }
        public String ForWhom { get; set; }
        public Int32 ForWhomId { get; set; }
        public ItFamilyCategory ItFamilyCategory { get; set; }
        public Int32? ItFamilyCategoryId { get; set; } 
    }
}