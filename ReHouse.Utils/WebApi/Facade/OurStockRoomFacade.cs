using System;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class OurStockRoomFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddOurStockRoom(String name, String adres, Int32 numberOfStock, string tokenHash)
        {
            var requestObj = new OurStockRoomRequest { Name = name, Adres = adres, NumberOfStock = numberOfStock, TokenHash = tokenHash };
            var response = await Post("api/OurStockRoom/AddOurStockRoom", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> DeleteOurStockRoom(int deleteId, string tokenHash)
        {
            var requestObj = new OurStockRoomRequest { SelectedId = deleteId, TokenHash = tokenHash };
            var response = await Post("api/OurStockRoom/DeleteOurStockRoom", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateOurStockRoom(String name, String adres, Int32 numberOfStock, Int32 selId, String tokenHash)
        {
            var requestObj = new OurStockRoomRequest { Name = name, Adres = adres, NumberOfStock = numberOfStock, SelectedId = selId, TokenHash = tokenHash };
            var response = await Post("api/OurStockRoom/UpdateOurStockRoom", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<OurStockRoomResponse> LoadOurStockRooms(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OurStockRoom/LoadOurStockRooms", requestObj, typeof(OurStockRoomResponse)).ConfigureAwait(false);

            var res = response as OurStockRoomResponse;
            return res;
        }

        public static async Task<OurStockRoomResponse> LoadItFamilyCategories(string tokenHash, int ourStockRoomId)
        {
            var requestObj = new OurStockRoomRequest { TokenHash = tokenHash, SelectedId = ourStockRoomId};
            var response = await Post("api/OurStockRoom/LoadItFamilyCategories", requestObj, typeof(OurStockRoomResponse)).ConfigureAwait(false);

            var res = response as OurStockRoomResponse;
            return res;
        }

        public static async Task<OurStockRoomResponse> GetOurStockRoom(string tokenHash, int selectedId)
        {
            var requestObj = new OurStockRoomRequest { TokenHash = tokenHash, SelectedId = selectedId};
            var response = await Post("api/OurStockRoom/GetOurStockRoom", requestObj, typeof(OurStockRoomResponse)).ConfigureAwait(false);

            var res = response as OurStockRoomResponse;
            return res;
        }
        public static async Task<OurStockRoomResponse> LoadStockProducts(string tokenHash, int categoryId, int ourStockRoomId)
        {
            var requestObj = new OurStockRoomRequest { TokenHash = tokenHash, SelectedId = categoryId, NumberOfStock = ourStockRoomId};
            var response = await Post("api/OurStockRoom/LoadStockProducts", requestObj, typeof(OurStockRoomResponse)).ConfigureAwait(false);

            var res = response as OurStockRoomResponse;
            return res;
        }
    }
}