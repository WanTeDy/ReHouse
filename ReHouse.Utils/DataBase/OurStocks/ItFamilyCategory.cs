using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.Filters;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.PriceRules;

namespace ITfamily.Utils.DataBase.OurStocks
{
    public class ItFamilyCategory : BaseObj
    {
        public virtual ItFamilyCategory Parent { get; set; }
        public Int32? ItFamilyParentId { get; set; }
        public Int32 ParentId { get; set; }
        public Int32 CategoryId { get; set; }
        [MaxLength(70)]
        public String Name { get; set; }
        public StockProduct BrainProduct { get; set; }
        [NotMapped]
        public BrainProductModel BrainProductModel { get; set; }
        [NotMapped]
        public Int32? BrainProduct_Id { get; set; }
        public Boolean HasRule { get; set; }
        public virtual List<RuleForPrice> RulesForPrice { get; set; }
        public virtual List<ItFamilyCategory> Categories { get; set; }
        public virtual List<StockProduct> StockProducts { get; set; }
        public virtual List<ItFamilyVendor> ItFamilyVendors { get; set; }
        public virtual List<ProductProperty> ProductProperties { get; set; }
        public FromWhatProvider FromWhatProvider { get; set; }
        public ItFamilyCategory()
        {
            Categories = new List<ItFamilyCategory>();
            StockProducts = new List<StockProduct>();
            ItFamilyVendors = new List<ItFamilyVendor>();
            ProductProperties = new List<ProductProperty>();
            HasRule = false;
        }
    }
}