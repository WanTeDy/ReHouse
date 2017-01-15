using System;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class OrderOutFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddNotes(string tokenHash, int orderId, string notes)
        {
            var requestObj = new OrderOutRequest { TokenHash = tokenHash, OrderId = orderId, Comment = notes };
            var response = await Post("api/OrderOut/AddNotes", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> ChangeStatusForOrderOut(string tokenHash, int orderId, OrderOutType orderOutType)
        {
            var requestObj = new OrderOutRequest { TokenHash = tokenHash, OrderId = orderId, OrderOutType = orderOutType};
            var response = await Post("api/OrderOut/ChangeStatusForOrderOut", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<OurOrderResponse> GetOneOrderOut(string tokenHash, int orderId)
        {
            var requestObj = new OrderOutRequest { TokenHash = tokenHash, OrderId = orderId};
            var response = await Post("api/OrderOut/GetOneOrderOut", requestObj, typeof(OurOrderResponse)).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
        public static async Task<OurOrderResponse> GetAllOrdersOut(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OrderOut/GetAllOrdersOut", requestObj, typeof(OurOrderResponse)).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }

        public static async Task<BaseResponse> OrderedOrdersComes(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OrderOut/OrderedOrdersComes", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> ShipOrderOut(string tokenHash, int orderId, int targetId, DateTime shipingDate, int accounting = -1)
        {
            var requestObj = new OrderOutRequest { TokenHash = tokenHash, OrderId = orderId, TargetId = targetId, ShipingDate = shipingDate, Accounting = accounting};
            var response = await Post("api/OrderOut/ShipOrderOut", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> ReserveOrderOut(string tokenHash, int orderId, int adressId, DateTime? reservedDate)
        {
            var requestObj = new OrderOutRequest { TokenHash = tokenHash, OrderId = orderId, AdressId = adressId, ReservedDate = reservedDate };
            var response = await Post("api/OrderOut/ReserveOrderOut", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<OurOrderResponse> PutOrderOut(string tokenHash, int orderComesId, int targetId, string currency, string comment, int adressId = -1)
        {
            var requestObj = new OrderOutRequest { OrderId = orderComesId, TokenHash = tokenHash, TargetId = targetId, Currency = currency, Comment = comment, AdressId = adressId};
            var response = await Post("api/OrderOut/PutOrderOut", requestObj, typeof(OurOrderResponse)).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
    }
}