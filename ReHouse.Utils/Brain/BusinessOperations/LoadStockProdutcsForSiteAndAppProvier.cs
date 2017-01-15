using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using ITfamily.Utils.Brain.Helper;
using ITfamily.Utils.BusinessOperations.BussOpWithDapper;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;

namespace ITfamily.Utils.Brain.BusinessOperations
{
    public class LoadStockProdutcsForSiteAndAppProvier : BaseEntityDapperOperation
    {
        //public List<BrainProduct> BrainProducts { get; set; }
        public List<BrainProductModel> BrainProductModels { get; set; }
        public Int32 CountPages { get; set; }
        public Int32 TotalItems { get; set; }

        private Int32 CategoryId { get; set; }
        //private Boolean CheckInStock { get; set; }
        private String TokenHash { get; set; }
        private Int32 Page { get; set; }
        private Int32 ItemsPerPage { get; set; }
        private ColumnSort ColumnSortOrder { get; set; }
        private String PropertyName { get; set; }
        private Boolean IsSite { get; set; }
        public LoadStockProdutcsForSiteAndAppProvier(int categoryId, int page, int itemsPerPage, string tokenHash, ColumnSort columnSortOrder, string propertyName, bool isSite = false)
        {
            CategoryId = categoryId;
            TokenHash = tokenHash;
            Page = page;
            ItemsPerPage = itemsPerPage;
            ColumnSortOrder = columnSortOrder;
            PropertyName = propertyName;
            //CheckInStock = false;
            IsSite = isSite;
        }

        public LoadStockProdutcsForSiteAndAppProvier(int categoryId, int page, int itemsPerPage, string tokenHash, bool isSite = true)
        {
            CategoryId = categoryId;
            TokenHash = tokenHash;
            Page = page;
            ItemsPerPage = itemsPerPage;
            ColumnSortOrder = ColumnSort.ASC;
            PropertyName = "Price";
            //CheckInStock = false;
            IsSite = isSite;
        }

        protected override void InTransaction()
        {
            var skip = (Page - 1)*ItemsPerPage;
            SqlMapper.GridReader prods = null;
            var orderByDirection = ColumnSortOrder == ColumnSort.ASC ? "ASC" : "DESC";
            var whereClause = "";
            if (!IsSite)
            {
                whereClause += "AND st.FromWhatProvider = " + (int) FromWhatProvider.Provider1;
                //PropertyName = "IsAvailable DESC, st.Price ";
                    // + " AND st.IsAvailable = 1";
            }
            else
            {
                PropertyName = "IsAvailable DESC, st.Price ";
                //whereClause += " AND st.IsAvailable = 1";
            }
            
            prods = Gateway.GetStockProductsFromCategory(CategoryId, skip, ItemsPerPage, PropertyName, orderByDirection, whereClause);

            var products = prods.Read<StockProduct>().ToList();
            TotalItems = prods.Read<int>().Single();
            CountPages = Convert.ToInt32(Math.Ceiling((double)TotalItems / ItemsPerPage));

            if(products == null || !products.Any()) return;
            BrainProductModels = new List<BrainProductModel>();

            var helper = new HelperPriceForListProduct(TokenHash, CategoryId, Context);
            helper.Init();

            foreach (var brainProduct in products)
            {
                var pr = helper.AddProductToList(brainProduct, false);
                if (pr != null)
                    BrainProductModels.Add(pr);
            }
        }
    }
}