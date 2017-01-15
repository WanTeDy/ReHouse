using System;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Request.Brain;
using ITfamily.Utils.WebApi.Response;
using ITfamily.Utils.WebApi.Response.Brain;

namespace ITfamily.Utils.WebApi.Facade.Brain
{
    public class BrainLoadFacade : BaseFacade
    {
        public static async Task<BrainProductsResponse> LoadMainPage(string tokenHash = "", bool check = true)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/BrainLoad/LoadMainPage", requestObj, typeof(BrainProductsResponse), check).ConfigureAwait(false);

            var res = response as BrainProductsResponse;
            return res;
        }
        public static async Task<BrainCategoriesResponse> LoadBrainCategories(bool check = false)
        {
            var requestObj = new BaseRequest();
            var response = await Post("api/BrainLoad/LoadBrainCategories", requestObj, typeof(BrainCategoriesResponse), check).ConfigureAwait(false);

            var res = response as BrainCategoriesResponse;
            return res;
        }

        public static async Task<BrainProductsResponse> LoadProductsPagination(int categoryId, int page, int itemsPerPage, string tokenHash="", bool check = true, bool isSite = true)
        {
            var requestObj = new BrainProductsRequest { CategoryId = categoryId, Page = page, ItemsPerPage = itemsPerPage, TokenHash = tokenHash, IsSite = isSite};
            var response = await Post("api/BrainLoad/LoadProductsPagination", requestObj, typeof(BrainProductsResponse), check).ConfigureAwait(false);

            var res = response as BrainProductsResponse;
            return res;
        }
        //LoadProductsPaginationWithSorting
        public static async Task<BrainProductsResponse> LoadProductsPaginationWithSorting(int categoryId, int page, int itemsPerPage, ColumnSort columnSort, string propertyName, string tokenHash = "", bool check = true, bool isSite = false)
        {
            var requestObj = new BrainProductsRequest { CategoryId = categoryId, Page = page, ItemsPerPage = itemsPerPage, TokenHash = tokenHash, PropertyName = propertyName, ColumnSortOrder = columnSort, IsSite = isSite};
            var response = await Post("api/BrainLoad/LoadProductsPaginationWithSorting", requestObj, typeof(BrainProductsResponse), check).ConfigureAwait(false);

            var res = response as BrainProductsResponse;
            return res;
        }

        //For Site
        public static async Task<BrainProductsResponse> LoadProductsWithPaginationForSite(int categoryId, int page, int itemsPerPage, string tokenHash = "", bool check = true, bool isSite = true)
        {
            var requestObj = new BrainProductsRequest { CategoryId = categoryId, Page = page, ItemsPerPage = itemsPerPage, TokenHash = tokenHash, IsSite = isSite };
            var response = await Post("api/BrainLoad/LoadProductsWithPaginationForSite", requestObj, typeof(BrainProductsResponse), check).ConfigureAwait(false);

            var res = response as BrainProductsResponse;
            return res;
        }
        //For Application for Provider1
        public static async Task<BrainProductsResponse> LoadProductsWithSortingForAppProvider(int categoryId, int page, int itemsPerPage, ColumnSort columnSort, string propertyName, string tokenHash = "", bool check = true, bool isSite = false)
        {
            var requestObj = new BrainProductsRequest { CategoryId = categoryId, Page = page, ItemsPerPage = itemsPerPage, TokenHash = tokenHash, PropertyName = propertyName, ColumnSortOrder = columnSort, IsSite = isSite };
            var response = await Post("api/BrainLoad/LoadProductsWithSortingForAppProvider", requestObj, typeof(BrainProductsResponse), check).ConfigureAwait(false);

            var res = response as BrainProductsResponse;
            return res;
        }
        
        
        public static async Task<BrainProductsResponse> LoadBrainProduct(int productId, string tokenHash, bool check=true)
        {
            var requestObj = new BrainProductsRequest {StockId = productId, TokenHash = tokenHash};
            var response = await Post("api/BrainLoad/LoadBrainProduct", requestObj, typeof(BrainProductsResponse), check).ConfigureAwait(false);

            var res = response as BrainProductsResponse;
            return res;
        }
        public static async Task<BrainProductsResponse> LoadBrainProductForReviewGood(string tokenHash, int productId, bool isBrainProductId, bool check = true)
        {
            var requestObj = new BrainProductsRequest { StockId = productId, TokenHash = tokenHash, IsBrainProductId = isBrainProductId};
            var response = await Post("api/BrainLoad/LoadBrainProductForReviewGood", requestObj, typeof(BrainProductsResponse), check).ConfigureAwait(false);

            var res = response as BrainProductsResponse;
            return res;
        }
        public static async Task<SearchResponse> SearchProducts(string tokenHash, string searchName, int categoryId = 0,  bool check = true)
        {
            var requestObj = new SearchRequest { TokenHash = tokenHash, SearchName = searchName, CategoryId = categoryId};
            var response = await Post("api/BrainLoad/SearchProducts", requestObj, typeof(SearchResponse), check).ConfigureAwait(false);

            var res = response as SearchResponse;
            return res;
        }
        public static async Task<PicturesResponse> LoadPicturesUrl(int productId, string tokenHash)
        {
            var requestObj = new BrainProductsRequest { StockId = productId, TokenHash = tokenHash };
            var response = await Post("api/BrainLoad/LoadPicturesUrl", requestObj, typeof(PicturesResponse), true).ConfigureAwait(false);

            var res = response as PicturesResponse;
            return res;
        }

        //Old Method for loadBrainProducts
        //public static async Task<BrainCategoriesResponse> LoadCategories(bool check = true)
        //{
        //    var requestObj = new BaseRequest();
        //    var response = await Post("api/BrainLoad/LoadCategories", requestObj, typeof(BrainCategoriesResponse), check).ConfigureAwait(false);
        //
        //    var res = response as BrainCategoriesResponse;
        //    return res;
        //}
        //public static async Task<BrainProductsResponse> LoadBrainProducts(int categoryId, string tokenHash="")
        //{
        //    var requestObj = new BrainProductsRequest { CategoryId = categoryId, TokenHash = tokenHash};
        //    var response = await Post("api/BrainLoad/LoadBrainProducts", requestObj, typeof(BrainProductsResponse)).ConfigureAwait(false);
        //
        //    var res = response as BrainProductsResponse;
        //    return res;
        //}
        //public static async Task<BrainProductsResponse> LoadBrainProducts(int categoryId, bool checkInStock, string tokenHash)
        //{
        //    var requestObj = new BrainProductsRequest { CategoryId = categoryId, CheckInStock = checkInStock, TokenHash = tokenHash };
        //    var response = await Post("api/BrainLoad/LoadBrainProducts", requestObj, typeof(BrainProductsResponse)).ConfigureAwait(false);
        //
        //    var res = response as BrainProductsResponse;
        //    return res;
        //}
    }
}