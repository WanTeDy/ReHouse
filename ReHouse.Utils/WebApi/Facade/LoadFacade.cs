using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.Filters;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class LoadFacade : BaseFacade
    {
        public static async Task<OurStockRoomResponse> GetFormedItfamilyCategories(bool isSite, bool isProvider, string tokenHash, bool check = true)
        {
            var requestObj = new ItfamilyCategoryRequest { TokenHash = tokenHash, IsSite = isSite, IsProvider = isProvider};
            var response = await Post("api/Load/GetFormedItfamilyCategories", requestObj, typeof(OurStockRoomResponse), check).ConfigureAwait(false);

            var res = response as OurStockRoomResponse;
            return res;
        }
        public static async Task<OurStockRoomResponse> SearchFromFilters(string tokenHash, List<FilterModel> filters, int categoryId, bool check = true)
        {
            var requestObj = new SearchRequest { TokenHash = tokenHash, CategoryId = categoryId, FilterModels = filters};
            var response = await Post("api/Load/SearchFromFilters", requestObj, typeof(OurStockRoomResponse), check).ConfigureAwait(false);

            var res = response as OurStockRoomResponse;
            return res;
        }
    }
}