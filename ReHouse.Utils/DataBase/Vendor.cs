using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITfamily.Utils.DataBase
{
    public class Vendor : BaseObj
    {
        [MaxLength(40)]
        public String name { get; set; }
        public Int32 vendorID { get; set; }
        [NotMapped]
        public Int32 categoryID { get; set; }
        public virtual List<BrainProduct> BrainProducts { get; set; }
        public virtual List<BrainCategory> BrainCategories { get; set; }
        //public Int32 categoryID { get; set; }
    }
}