using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.Filters;

namespace ITfamily.Utils.BusinessOperations.OurStock.Properties
{
    public class LoadProperiesForCategoryOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public Int32 CategoryId { get; set; }
        public List<ProductProperty> ProductProperties { get; set; }
        public String CategoryName { get; set; }
        public LoadProperiesForCategoryOperation(string tokenHash, int categoryId)
        {
            TokenHash = tokenHash;
            CategoryId = categoryId;
        }

        protected override void InTransaction()
        {
            var props = Context.ProductProperties.Where(x => !x.Deleted && x.CategoryId == CategoryId).ToList();
            ProductProperties = props.Select(x => new ProductProperty
            {
                Id = x.Id,
                PropertyName = x.PropertyName,
            }).ToList();
            var cat = Context.ItFamilyCategories.FirstOrDefault(x => !x.Deleted && x.Id == CategoryId);
            if (cat != null)
            {
                CategoryName = cat.Name;
            }
        }
    }
}