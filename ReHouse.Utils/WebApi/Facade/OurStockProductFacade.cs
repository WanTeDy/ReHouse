using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class OurStockProductFacade : BaseFacade
    {
        public static async Task<BaseResponse> SetOurPriceForStockProduct(string tokenHash, int productId, bool isPriceForOneProduct, decimal priceUsdForManager, decimal priceUsdForPartner, decimal priceUsdForClients)
        {
            var requestObj = new OurStockProductRequest { TokenHash = tokenHash, SelectedId = productId, IsPriceForOneProduct = isPriceForOneProduct, PriceUsdForManager = priceUsdForManager, PriceUsdForClients = priceUsdForClients, PriceUsdForPartner = priceUsdForPartner };
            var response = await Post("api/OurStockProduct/SetOurPriceForStockProduct", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<OurStockProductResponse> LoadStockProducts(string tokenHash, int categoryId, int ourStockRoomId)
        {
            var requestObj = new OurStockProductRequest { TokenHash = tokenHash, SelectedId = categoryId, NumberOfStock = ourStockRoomId };
            var response = await Post("api/OurStockProduct/LoadStockProducts", requestObj, typeof(OurStockProductResponse)).ConfigureAwait(false);

            var res = response as OurStockProductResponse;
            return res;
        }
        public static async Task<OurStockProductResponse> LoadStockProductByProductId(string tokenHash, int productId)
        {
            var requestObj = new OurStockProductRequest { TokenHash = tokenHash, SelectedId = productId };
            var response = await Post("api/OurStockProduct/LoadStockProductByProductId", requestObj, typeof(OurStockProductResponse)).ConfigureAwait(false);

            var res = response as OurStockProductResponse;
            return res;
        }
        public static async Task<OurStockProductResponse> LoadStockProductById(string tokenHash, int stockProductId)
        {
            var requestObj = new OurStockProductRequest { TokenHash = tokenHash, SelectedId = stockProductId };
            var response = await Post("api/OurStockProduct/LoadStockProductById", requestObj, typeof(OurStockProductResponse)).ConfigureAwait(false);

            var res = response as OurStockProductResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateStockProduct(string tokenHash, StockProduct stockProduct, byte[] mainImageBytes)
        {
            var requestObj = new OurStockProductRequest { TokenHash = tokenHash, StockProduct = stockProduct, MainImageBytes = mainImageBytes };
            var response = await Post("api/OurStockProduct/UpdateStockProduct", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> AddStockProduct(string tokenHash, DataBase.OurStocks.StockProduct stockProductModel, byte[] mainImageBytes)
        {
            var requestObj = new OurStockProductRequest { TokenHash = tokenHash, StockProduct = stockProductModel, MainImageBytes = mainImageBytes };
            var response = await Post("api/OurStockProduct/AddStockProduct", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> DeleteStockProduct(string tokenHash, int deleteId)
        {
            var requestObj = new OurStockProductRequest { TokenHash = tokenHash, SelectedId = deleteId };
            var response = await Post("api/OurStockProduct/DeleteStockProduct", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}