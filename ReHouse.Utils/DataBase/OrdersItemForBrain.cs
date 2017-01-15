using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITfamily.Utils.DataBase
{
    public class OrdersItemForBrain : BaseObj
    {
        [NotMapped]
        public String ProductName { get; set; }
        public Int32 productID { get; set; }
        [MaxLength(20)]
        public String product_code { get; set; }
        [MaxLength(80)]
        public String articul { get; set; }
        public Int32 quantity { get; set; }
        [MaxLength(255)]
        public String comment { get; set; }
        public Decimal price { get; set; }
        public Decimal price_uah { get; set; }
        public Int32? UnitOfCommodityId { get; set; }
        [NotMapped]
        public String IsUnitOfCommodity { get; set; }
        [NotMapped]
        public Int32 OrderComesId { get; set; }
    }
}