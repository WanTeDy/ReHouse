using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class OrderComesFacade : BaseFacade
    {
        public static async Task<AnalyzeResponse> AnalyzeGoods(string tokenHash, DateTime @from, DateTime to, decimal fromPrice, decimal toPrice, int categoryId, FromWhatProvider fromWhat)
        {
            var requestObj = new AnalyzeRequest { TokenHash = tokenHash, From = from, To = to, FromPrice = fromPrice, ToPrice = toPrice, CategoryId = categoryId, FromWhat = fromWhat };
            var response = await Post("api/OrderComes/AnalyzeGoods", requestObj, typeof(AnalyzeResponse)).ConfigureAwait(false);

            var res = response as AnalyzeResponse;
            return res;
        }
        public static async Task<BaseResponse> AddNotes(string notes, int orderComesId, string tokenHash, bool check = true)
        {
            var requestObj = new OurOrderRequest { Comment = notes, TokenHash = tokenHash, SelectedId = orderComesId};
            var response = await Post("api/OrderComes/AddNotes", requestObj, typeof(BaseResponse), check).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> AddManagerToOrderComes(int orderComesId, string tokenHash, bool check = true)
        {
            var requestObj = new OurOrderRequest { TokenHash = tokenHash, SelectedId = orderComesId };
            var response = await Post("api/OrderComes/AddManagerToOrderComes", requestObj, typeof(BaseResponse), check).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<OurOrderResponse> AddProductToOrder(DataPostOrder dataPostOrder, string tokenHash, bool check = true)
        {
            var requestObj = new OurOrderRequest { DataPostOrder = dataPostOrder, TokenHash = tokenHash };
            var response = await Post("api/OrderComes/AddProductToOrder", requestObj, typeof(OurOrderResponse), check).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
        public static async Task<OurOrderResponse> GetAllOrdersComes(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OrderComes/GetAllOrdersComes", requestObj, typeof(OurOrderResponse)).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
        public static async Task<OurOrderResponse> GetHelerOrdersData(string tokenHash, bool check = true)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OrderComes/GetHelerOrdersData", requestObj, typeof(OurOrderResponse), check).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
        public static async Task<OurOrderResponse> GetOrderComes(string tokenHash, bool check = true)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OrderComes/GetOrderComes", requestObj, typeof(OurOrderResponse), check).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }

        public static async Task<OurOrderResponse> GetOrderComesProductModel(string tokenHash, bool check = true)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OrderComes/GetOrderComesProductModel", requestObj, typeof(OurOrderResponse), check).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }

        public static async Task<OurOrderResponse> GetDataForCameTheOrder(string tokenHash, int selectedOrderComesId)
        {
            var requestObj = new OurOrderRequest { TokenHash = tokenHash, SelectedId = selectedOrderComesId };
            var response = await Post("api/OrderComes/GetDataForCameTheOrder", requestObj, typeof(OurOrderResponse)).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
        public static async Task<OurOrderResponse> GetAllDataBeforePartnerPutOrder(string tokenHash, bool check = true)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/OrderComes/GetAllDataBeforePartnerPutOrder", requestObj, typeof(OurOrderResponse), check).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
        public static async Task<BaseResponse> SaveChangesOfStatesOrderComes(int selectedOrderComesId, OrderType orderType, PaymentStatus paymentStatus, string tokenHash)
        {
            var requestObj = new OurOrderRequest { TokenHash = tokenHash, SelectedId = selectedOrderComesId, PaymentStatus = paymentStatus, OrderType = orderType };
            var response = await Post("api/OrderComes/SaveChangesOfStatesOrderComes", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> SaveDeliveryDateOfOrderComes(int selectedOrderComesId, DateTime deliveryDate, string tokenHash)
        {
            var requestObj = new OurOrderRequest { TokenHash = tokenHash, SelectedId = selectedOrderComesId, DeliveryDate = deliveryDate };
            var response = await Post("api/OrderComes/SaveDeliveryDateOfOrderComes", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<OurOrderResponse> DeleteProductFromOrder(int productId, string tokenHash, bool check = true)
        {
            var requestObj = new OurOrderRequest { SelectedId = productId, TokenHash = tokenHash };
            var response = await Post("api/OrderComes/DeleteProductFromOrder", requestObj, typeof(OurOrderResponse), check).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
        public static async Task<OurOrderResponse> ChangeQuantity(int productId, int quantity, string tokenHash, bool check = true)
        {
            var requestObj = new OurOrderRequest { SelectedId = productId, Quantity = quantity, TokenHash = tokenHash };
            var response = await Post("api/OrderComes/ChangeQuantity", requestObj, typeof(OurOrderResponse), check).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }

        public static async Task<BaseResponse> OrderingGoods(string tokenHash, DeliveryType deliveryType, PaymentMethod paymentMethod, CurrencyType currencyType, string comment, string adress, bool check = true)
        {
            var requestObj = new OurOrderRequest { DeliveryType = deliveryType, PaymentMethod = paymentMethod, CurrencyType = currencyType, Comment = comment, Adress = adress, TokenHash = tokenHash };
            var response = await Post("api/OrderComes/OrderingGoods", requestObj, typeof(BaseResponse), check).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> OrderGoodsForClient(string tokenHash, DeliveryType deliveryType, PaymentMethod paymentMethod, string comment, string adress, string email, string fio, string phone, bool check = true)
        {
            var requestObj = new OurOrderRequest { DeliveryType = deliveryType, PaymentMethod = paymentMethod, Comment = comment, Adress = adress, TokenHash = tokenHash, Email = email, FIO = fio, Phone = phone};
            var response = await Post("api/OrderComes/OrderGoodsForClient", requestObj, typeof(BaseResponse), check).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<BaseResponse> MoveGoodsToOurStockRoom(List<OrderItem> ordersItems, int selectedOurStockRoomId, int orderedId, int orderOutId, string tokenHash)
        {
            var requestObj = new OurOrderRequest { OrderItems = ordersItems, TokenHash = tokenHash, SelectedId = selectedOurStockRoomId, OrderComeId = orderedId, OrderOutId = orderOutId };
            var response = await Post("api/OrderComes/MoveGoodsToOurStockRoom", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<OurOrderResponse> CheckNewOrderComes(int nowExist, string tokenHash)
        {
            var requestObj = new OurOrderRequest { SelectedId = nowExist, TokenHash = tokenHash };
            var response = await Post("api/OrderComes/CheckNewOrderComes", requestObj, typeof(OurOrderResponse)).ConfigureAwait(false);

            var res = response as OurOrderResponse;
            return res;
        }
    }
}