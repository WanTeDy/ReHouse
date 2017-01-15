using System.Threading.Tasks;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.OurStocks;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class OurStockFacade : BaseFacade
    {
        //1
        public static async Task<BaseResponse> AddItfamilyCategory(string tokenHash, int? itfamilyParentId, string nameCategory)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, ItfamilyParentId = itfamilyParentId, NewName = nameCategory };
            var response = await Post("api/OurStock/AddItfamilyCategory", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //2
        public static async Task<BaseResponse> DeleteItfamilyCategory(string tokenHash, int deletedId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = deletedId };
            var response = await Post("api/OurStock/DeleteItfamilyCategory", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //3
        public static async Task<BaseResponse> UpdateItfamilyCategory(string tokenHash, int itfamilyCategoryId, string newNameCategory)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = itfamilyCategoryId, NewName = newNameCategory };
            var response = await Post("api/OurStock/UpdateItfamilyCategory", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //4
        public static async Task<BaseResponse> AddItfamilyVendor(string tokenHash, string vendorName, int itfamilyCategoryId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = itfamilyCategoryId, NewName = vendorName };
            var response = await Post("api/OurStock/AddItfamilyVendor", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //5
        public static async Task<BaseResponse> DeletedItfamilyVendor(string tokenHash, int deletedId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = deletedId };
            var response = await Post("api/OurStock/DeletedItfamilyVendor", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //6
        public static async Task<BaseResponse> UpdateItfamilyVendor(string tokenHash, int itfamilyVendorId, string newVendorName)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = itfamilyVendorId, NewName = newVendorName };
            var response = await Post("api/OurStock/UpdateItfamilyVendor", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //7
        public static async Task<OurStockResponse> LoadItfamilyVendors(string tokenHash, int itfamilyCategoryId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = itfamilyCategoryId };
            var response = await Post("api/OurStock/LoadItfamilyVendors", requestObj, typeof(OurStockResponse)).ConfigureAwait(false);

            var res = response as OurStockResponse;
            return res;
        }
        //8
        public static async Task<OurStockResponse> LoadOneUnitOfCommodity(string tokenHash, int unitOfCommodityId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = unitOfCommodityId };
            var response = await Post("api/OurStock/LoadOneUnitOfCommodity", requestObj, typeof(OurStockResponse)).ConfigureAwait(false);

            var res = response as OurStockResponse;
            return res;
        }
        //9
        public static async Task<OurStockResponse> LoadUnitOfCommodities(string tokenHash, int stockProductId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = stockProductId };
            var response = await Post("api/OurStock/LoadUnitOfCommodities", requestObj, typeof(OurStockResponse)).ConfigureAwait(false);

            var res = response as OurStockResponse;
            return res;
        }
        //10
        public static async Task<OurStockResponse> AddUnitOfCommodity(string tokenHash, UnitOfCommodity unitOfCommodity, int fromOrderOutId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, UnitOfCommodity = unitOfCommodity, FromOrderOutId = fromOrderOutId };
            var response = await Post("api/OurStock/AddUnitOfCommodity", requestObj, typeof(OurStockResponse)).ConfigureAwait(false);

            var res = response as OurStockResponse;
            return res;
        }
        //11
        public static async Task<BaseResponse> UpdateUnitOfCommodity(string tokenHash, UnitOfCommodity unitOfCommodity)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, UnitOfCommodity = unitOfCommodity};
            var response = await Post("api/OurStock/UpdateUnitOfCommodity", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //12
        public static async Task<OurStockResponse> LoadProperiesForCategory(string tokenHash, int categoryId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = categoryId };
            var response = await Post("api/OurStock/LoadProperiesForCategory", requestObj, typeof(OurStockResponse)).ConfigureAwait(false);

            var res = response as OurStockResponse;
            return res;
        }
        //13
        public static async Task<OurStockResponse> LoadFilledPropertiesForProduct(string tokenHash, int stockProductId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = stockProductId };
            var response = await Post("api/OurStock/LoadFilledPropertiesForProduct", requestObj, typeof(OurStockResponse)).ConfigureAwait(false);

            var res = response as OurStockResponse;
            return res;
        }
        //14
        public static async Task<BaseResponse> AddOnePropertyForStockProduct(string tokenHash, PropertyValueModel propertyValueModel)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, PropertyValueModel = propertyValueModel };
            var response = await Post("api/OurStock/AddOnePropertyForStockProduct", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //15
        public static async Task<BaseResponse> UpdateOnePropertyForStockProduct(string tokenHash, PropertyValueModel propertyValueModel)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, PropertyValueModel = propertyValueModel };
            var response = await Post("api/OurStock/UpdateOnePropertyForStockProduct", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //16
        public static async Task<BaseResponse> DeleteOnePropertyForStockProduct(string tokenHash, int deletePropValueId)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = deletePropValueId };
            var response = await Post("api/OurStock/DeleteOnePropertyForStockProduct", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        //17
        public static async Task<BaseResponse> UnitOfCommodityReservation(string tokenHash, int unitOfCommodityId, int orderComesId, int reservedQuantity)
        {
            var requestObj = new OurStockRequest { TokenHash = tokenHash, SelectedId = unitOfCommodityId, OrderComesId = orderComesId, Quantity = reservedQuantity };
            var response = await Post("api/OurStock/UnitOfCommodityReservation", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}