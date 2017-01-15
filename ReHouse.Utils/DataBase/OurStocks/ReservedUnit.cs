using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.OtherOurDataForDb;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class ReservedUnit
    {
        public Int32 Id { get; set; }
        public Int32 OrderComesId { get; set; }
        public Int32 UnitOfCommodityId { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 NeededQuantity { get; set; }
        public Boolean Deleted { get; set; }
        public virtual OrderComes OrderComes { get; set; }
        public virtual UnitOfCommodity UnitOfCommodity { get; set; }

    }
}