using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;

namespace ITfamily.Utils.DataBase
{
    public class BrainCategory : BaseObj
    {
        //http://api.brain.com.ua/categories/SID 
        //httpGet

        public virtual BrainCategory Parent { get; set; }
        public Int32? BrainParentID { get; set; }
        public Int32 parentID { get; set; }
        public Int32 categoryID { get; set; }
        [MaxLength(70)]
        public String name { get; set; }
        public virtual List<BrainCategory> Categories { get; set; }
        public virtual List<BrainProduct> BrainProducts { get; set; }
        public virtual List<Vendor> Vendors { get; set; }
        public BrainCategory()
        {
            Categories = new List<BrainCategory>();
            BrainProducts = new List<BrainProduct>();
            Vendors = new List<Vendor>();
        }
    }
}