using System;
using System.ComponentModel.DataAnnotations;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.DataBase.CreditInformation
{
    public class CustomerCard : BaseObj
    {
        public virtual Contractor Contractor { get; set; }
        //public virtual Entrepreneur Entrepreneur { get; set; }
        public Int32 ContractorId { get; set; }
        //public Int32 PartnerId { get; set; }
        //public Decimal Limit { get; set; }
        public DateTime DateTime { get; set; }
        public Decimal? ComesUsd { get; set; }
        public Decimal? Outgo { get; set; }
        public Decimal Balance { get; set; }
        public virtual OrderComes OrderComes { get; set; }
        //public Int32? OrderComesId { get; set; }
        public virtual ComesMoney ComesMoney { get; set; }
        //public Int32? ComesMoneyId { get; set; }
        [MaxLength(500)]
        public String Notes { get; set; }
    }
}