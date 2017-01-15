using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.CreditInformation;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.DataBase.Security;

namespace ITfamily.Utils.DataBase.ModelForUI
{
    public class CustomerCardModel
    {
        public Int32 Id { get; set; }
        //public virtual Entrepreneur Entrepreneur { get; set; }
        //public Int32 PartnerId { get; set; }
        public Contractor Contractor { get; set; }
        public Int32 ContractorId { get; set; }
        //public Decimal Limit { get; set; }
        public DateTime DateTime { get; set; }
        public Decimal? ComesUsd { get; set; }
        public Decimal? Outgo { get; set; }
        public Decimal Balance { get; set; }
        public List<OrderComes> OrderComes { get; set; }
        //public Int32? OrderComesId { get; set; }
        public List<ComesMoney> ComesMoney { get; set; }
        //public Int32? ComesMoneyId { get; set; }
        public String Notes { get; set; } 
    }
}