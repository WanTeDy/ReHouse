using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITfamily.Utils.DataBase
{
    public class BrainStocks : BaseObj
    {
        public Int32 stockID { get; set; }
        [MaxLength(70)]
        public String name { get; set; }

        public virtual List<BrainProduct> BrainProducts { get; set; }
    }
}