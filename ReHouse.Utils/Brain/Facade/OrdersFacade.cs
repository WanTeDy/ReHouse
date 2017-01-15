using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.Brain.Models;
using ITfamily.Utils.Brain.Request;
using ITfamily.Utils.Brain.Response;
using ITfamily.Utils.Brain.Response.Models;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.Except.ApiException;
using ITfamily.Utils.WebApi.Facade;
using Newtonsoft.Json;

namespace ITfamily.Utils.Brain.Facade
{
    public class OrdersFacade : BaseBrainFacade
    {
        /// <summary>
        /// Метод для добавления товаров в текущий заказ. Если товар уже присутствует в заказе, 
        /// он обновляется в соответствии с отправленными данными. 
        /// </summary>
        /// <param name="datePost">Товар может быть идентифицирован по productID, product_code, 
        /// articul либо любому их сочетанию. Т.е. обязательно должен быть указан хотя бы один 
        /// из этих параметров. </param>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns>Возвращает объект со статусом выполнения операции (1 если успешно) </returns>
        public static async Task<BaseBrainResponse> PostOrder(List<DataPostOrder> datePost, string sid)
        {
            var str = JsonConvert.SerializeObject(datePost);
            var requestObj = new OrderPostRequest { data = str };
            var response = await Post("order/"+sid, requestObj, typeof(BaseBrainResponse)).ConfigureAwait(false);

            var res = response as BaseBrainResponse;
            if (res == null)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "Func PostOrder: \n" + str, State.Error).Result;
                throw new NoServerResponseException();
            }
            if (res.status != 1 && res.result != "1")
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func PostOrder: \n" + str, State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }
        
        /// <summary>
        /// Метод для удаления товаров из текущего заказа. 
        /// Для удаления товаров нужно отправить POST запрос с колекцией DataPostDeleteOrder.
        /// </summary>
        /// <param name="datePostDelete">Товар может быть идентифицирован по productID либо product_code.
        /// Т.е. обязательно должен быть указан один из этих параметров.</param>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns>Возвращает объект со статусом выполнения операции (1 если успешно) </returns>
        public static async Task<BaseBrainResponse> PostDeleteOrder(List<DataPostDeleteOrder> datePostDelete, string sid)
        {
            var str = JsonConvert.SerializeObject(datePostDelete);
            var requestObj = new OrderPostRequest { data = str };
            var response = await Post("order/delete/" + sid, requestObj, typeof(BaseBrainResponse)).ConfigureAwait(false);

            var res = response as BaseBrainResponse;
            if (res == null)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "Func PostDeleteOrder: \n" + str, State.Error).Result;
                throw new NoServerResponseException();
            }
            if (res.status != 1 && res.result != "1")
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func PostDeleteOrder: \n" + str, State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }

        /// <summary>
        /// метод для получения списка товаров из текущего заказа
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns></returns>
        public static async Task<List<OrdersItemForBrain>> GetOrder(string sid)
        {
            var response = await Get("order/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            OrderResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<OrderResponse>(response);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    var r = LoggFacade.Error(response, "Func: GetOrder \n" + ex.Message + "\nStackTrace: " + ex.StackTrace).Result;
                }
                else if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException == null)
                    {
                        var r = LoggFacade.Error(response, "Func: GetOrder \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace).Result;
                    }
                    else
                    {
                        var r = LoggFacade.Error(response, "Func: GetOrder \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace + " \nin.InnerException: " + ex.InnerException.InnerException.Message + " \nStackTrace: " + ex.InnerException.InnerException.StackTrace).Result;
                    }
                }
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetOrder \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            return res.result;
        }

        /// <summary>
        /// Метод для получения списка заказов.
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <param name="type">тип заказа</param>
        /// <param name="offset">смещение (количество заказов, пропускаемых перед выводом результатов; по умолчанию - 0)</param>
        /// <param name="startDate">начальная дата</param>
        /// <param name="endDate">конечная дата, не позже чем сегодня</param>
        /// <returns>Метод возвращает список заказов. 
        /// Если в запросе присутствуют необязательные параметры, то список заказов фильтруется в 
        /// соответствии с ними. Параметр "count" результата показывает количество заказов без 
        /// учета значений параметров offset и limit. Максимальное количество вывода 10.</returns>
        public static async Task<OrdersHelperResponse> GetOrders(string sid, string type, int offset = -1, DateTime? startDate = null, DateTime? endDate = null)
        {
            var str = "orders/" + sid;
            if (type != "default")
            {
                str += "?type=" + type;
                if (startDate.HasValue)
                    str += "&date_start=" + startDate.Value.ToString("dd.MM.yyyy");
                if (endDate.HasValue)
                    str += "&date_finish=" + endDate.Value.ToString("dd.MM.yyyy");
                if (offset != -1)
                    str += "&offset=" + offset;
            }
            else
            {
                if (startDate.HasValue)
                {
                    str += "?date_start=" + startDate.Value.ToString("dd.MM.yyyy");
                    if (endDate.HasValue)
                        str += "&date_finish=" + endDate.Value.ToString("dd.MM.yyyy");
                    if (offset != -1)
                        str += "&offset=" + offset;
                }
                else if (endDate.HasValue)
                {
                    str += "?date_finish=" + endDate.Value.ToString("dd.MM.yyyy");
                    if(offset != -1)
                        str += "&offset=" + offset;
                }
                else if (offset != -1)
                    str += "?offset=" + offset;
            }
            var response = await Get(str).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            OrdersResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<OrdersResponse>(response);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    var r = LoggFacade.Error(response, "Func: GetOrders \n" + ex.Message + "\nStackTrace: " + ex.StackTrace).Result;
                }
                else if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException == null)
                    {
                        var r = LoggFacade.Error(response, "Func: GetOrders \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace).Result;
                    }
                    else
                    {
                        var r = LoggFacade.Error(response, "Func: GetOrders \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace + " \nin.InnerException: " + ex.InnerException.InnerException.Message + " \nStackTrace: " + ex.InnerException.InnerException.StackTrace).Result;
                    }
                }
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetOrders \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            return res.result;
        }

        /// <summary>
        /// Метод для получения списка пунктов выдачи / служб доставки 
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns></returns>
        public static async Task<TargetsResponse> GetTargets(string sid)
        {
            var response = await Get("targets/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            TargetsResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<TargetsResponse>(response);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    var r = LoggFacade.Error(response, "Func: GetTargets \n" + ex.Message + "\nStackTrace: " + ex.StackTrace).Result;
                }
                else if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException == null)
                    {
                        var r = LoggFacade.Error(response, "Func: GetTargets \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace).Result;
                    }
                    else
                    {
                        var r = LoggFacade.Error(response, "Func: GetTargets \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace + " \nin.InnerException: " + ex.InnerException.InnerException.Message + " \nStackTrace: " + ex.InnerException.InnerException.StackTrace).Result;
                    }
                }
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error( JsonConvert.SerializeObject(res), "Func: GetTargets \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                //return null;
            }
            return res;
        }

        /// <summary>
        /// Метод возвращает список адресов доставки с названиями ("name"),
        /// уникальными идентификаторами ("addressID") адресами ("address") и 
        /// идентификаторами служб ("targets"), которые могут осуществить доставку 
        /// по указанному адресу. 
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns></returns>
        public static async Task<AddressesResponse> GetAddresses(string sid)
        {
            var response = await Get("addresses/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            AddressesResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<AddressesResponse>(response);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    var r = LoggFacade.Error(response, "Func: GetAddresses \n" + ex.Message + "\nStackTrace: " + ex.StackTrace).Result;
                }
                else if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException == null)
                    {
                        var r = LoggFacade.Error(response, "Func: GetAddresses \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace).Result;
                    }
                    else
                    {
                        var r = LoggFacade.Error(response, "Func: GetAddresses \n" + ex.Message + "\nStackTrace: " + ex.StackTrace,
                            ex.InnerException.Message + " \nStackTrace:" + ex.InnerException.StackTrace + " \nin.InnerException: " + ex.InnerException.InnerException.Message + " \nStackTrace: " + ex.InnerException.InnerException.StackTrace).Result;
                    }
                }
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetAddresses \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }

        /// <summary>
        /// Метод для оформления текущего заказа. 
        /// Список товаров берется из текущего заказа. 
        /// Регион и метод доставки выбираются по указанному targetID. 
        /// </summary>
        /// <param name="datePutOrder">Параметры POST запроса: обязательные currency и targetID</param>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns>Метод возвращает идентификатор оформленного заказа.</returns>
        public static async Task<OrderPutResponse> PutOrder(DataPutOrder datePutOrder, string sid)
        {
            //var str = JsonConvert.SerializeObject(datePutOrder);
            //var requestObj = new OrderPostRequest { data = str };
            var response = await Post("order/put/" + sid, datePutOrder, typeof(OrderPutResponse)).ConfigureAwait(false);
            var res = response as OrderPutResponse;
            if (res == null)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "Func PutOrder: \n" + JsonConvert.SerializeObject(datePutOrder), State.Error).Result;
                throw new NoServerResponseException();
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func PutOrder: \n" + JsonConvert.SerializeObject(datePutOrder), State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }

        /// <summary>
        /// Метод для бронирования заказа. 
        /// </summary>
        /// <param name="orderId">идентификатор заказа</param>
        /// <param name="sid">идентификатор сессии</param>
        /// <param name="reserveddate">дата бронирования (по умолчанию - +1 сутки к текущей дате),
        /// не обязательный параметр</param>
        /// <param name="clientId">идентификатор клиента, не обязательный параметр</param>
        /// <returns>Метод возвращает статус выполнения операции. (1 если успешно)</returns>
        public static async Task<BaseBrainResponse> ReserveOrder(int orderId, string sid, string login, string passHash, DateTime? reserveddate = null, int clientId = -1)
        {
            var requestObj = new OrderReserveRequest();
            //if (reserveddate.HasValue)
            //    requestObj.reserveddate = reserveddate.Value.ToString("dd.MM.yyyy");
            //requestObj.login = ConstV.
            //if (clientId != -1)
            //    requestObj.clientID = clientId;
            requestObj.login = login;
            requestObj.password = passHash;
            var response = await Post("order/" + orderId + "/reserve/" + sid, requestObj, typeof(BaseBrainResponse)).ConfigureAwait(false);

            var res = response as BaseBrainResponse;
            if (res == null)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "Func ReserveOrder: \nOrderID=" + orderId + " SerializePostObject=" +
                JsonConvert.SerializeObject(requestObj), State.Error).Result;
                throw new NoServerResponseException();
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func ReserveOrder: \nOrderID=" + orderId + " SerializePostObject=" +
                JsonConvert.SerializeObject(requestObj), State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }

        /// <summary>
        /// Метод для отгрузки заказа. 
        /// </summary>
        /// <param name="orderId">Идентификатор заказа</param>
        /// <param name="sid">идентификатор сессии</param>
        /// <param name="shipingdate">дата и время отгрузки</param>
        /// <param name="accounting">необходимость бухучета (1-да, 0-нет), не обязательный параметр</param>
        /// <param name="subsidiaryId">идентификатор филиала компании, не обязательный параметр</param>
        /// <param name="clientId">идентификатор клиента, не обязательный параметр</param>
        /// <returns>Метод возвращает статус выполнения операции. (1 если успешно) </returns>
        public static async Task<BaseBrainResponse> ShipOrder(int orderId, string sid, int subsidiaryId, DateTime shipingdate, int accounting = -1, int clientId = -1)
        {
            var requestObj = new OrderShipRequest
            {
                shipingdate = shipingdate.ToString("dd.MM.yyyy HH:mm")
            };
            if (accounting != -1)
                requestObj.accounting = accounting;
            //if (subsidiaryId != -1)
            //    requestObj.subsidiaryID = subsidiaryId;
            //if (clientId != -1)
            //    requestObj.clientID = clientId;

            var response = await Post("order/" + orderId + "/ship/" + sid, requestObj, typeof(BaseBrainResponse)).ConfigureAwait(false);

            var res = response as BaseBrainResponse;
            if (res == null)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "Func ShipOrder: \nOrderID=" + orderId + " SerializePostObject=" +
                JsonConvert.SerializeObject(requestObj), State.Error).Result;
                throw new NoServerResponseException();
            }
            if (res.status != 1)
            {
                var r =
                    LoggFacade.Error(JsonConvert.SerializeObject(res),
                        "Func ShipOrder: \nOrderID=" + orderId + " SerializePostObject=" +
                        JsonConvert.SerializeObject(requestObj), State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }
    }
}