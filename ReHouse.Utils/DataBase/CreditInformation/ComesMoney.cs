using System;
using System.ComponentModel.DataAnnotations;
using ITfamily.Utils.DataBase.AuxiliaryData;

namespace ITfamily.Utils.DataBase.CreditInformation
{
    public class ComesMoney : BaseObj
    {
        public DateTime DateTime { get; set; }
        [MaxLength(200)]
        public String DetailsOfPayment { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public Decimal Amount { get; set; }
        public virtual CustomerCard CustomerCard { get; set; }
    }
}