using System;
using System.Collections.Generic;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.DataBase.Filters
{
    public class ProductProperty
    {
        public Int32 Id { get; set; }
        public virtual ItFamilyCategory ItFamilyCategory { get; set; }
        public Int32 CategoryId { get; set; }
        public String PropertyName { get; set; }
        public virtual List<ProductPropertyValues> ProductPropertyValues { get; set; }
        public Boolean Deleted { get; set; }
        public Boolean Hidden { get; set; }
    }
}