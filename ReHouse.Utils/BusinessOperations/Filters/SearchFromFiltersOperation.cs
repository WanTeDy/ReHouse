using System;
using System.Collections.Generic;
using System.Linq;
using ITfamily.Utils.DataBase.Filters;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.BusinessOperations.Filters
{
    public class SearchFromFiltersOperation : BaseOperation
    {
        public String TokenHash { get; set; }
        public List<FilterModel> FilterModels { get; set; }
        public List<StockProduct> StockProducts { get; set; }
        public Int32 CategoryId { get; set; }
        public SearchFromFiltersOperation(string tokenHash, int categoryId, List<FilterModel> filterModels)
        {
            TokenHash = tokenHash;
            FilterModels = filterModels;
            CategoryId = categoryId;
        }

        protected override void InTransaction()
        {
            var contr = Context.Contractors.FirstOrDefault(x => x.IsActive && !x.Deleted && x.TokenHash == TokenHash);
            var products = Context.StockProducts.Where(x=>!x.Deleted && x.IsAvailable && x.ItFamilyCategoryId == CategoryId);
            foreach(var prop in FilterModels.Where(x=>!string.IsNullOrEmpty(x.Value)))
            {
                products =
                    products.Where(x => x.PropertyValueses.Any(y => y.PropertyId == prop.PropertyId && y.Value.Contains(prop.Value)));
            }
            var prod = Context.StockProducts.Where(x => !x.Deleted && x.IsAvailable && x.ItFamilyCategoryId == CategoryId && x.PropertyValueses.Any(y=>y.PropertyId==1 && y.Value == "PORTO")).ToList();
            StockProducts = products.ToList();

        }
    }
}