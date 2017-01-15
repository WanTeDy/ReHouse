using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITfamily.Utils.Brain.Response;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.AuxiliaryModels;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.Except.ApiException;
using ITfamily.Utils.WebApi.Facade;
using Newtonsoft.Json;

namespace ITfamily.Utils.Brain.Facade
{
    public class BrainCommonFacade : BaseBrainFacade
    {
        public static async Task<BrainStocksResponse> GetStocks(string sid)
        {
            var response = await Get("stocks/"+sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            BrainStocksResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<BrainStocksResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetStocks \nMessageEx: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                //Logg.Error(JsonConvert.SerializeObject(res), "Func: GetStocks \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer);
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetStocks \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            return res;
        }

        public static async Task<List<BrainCategory>> GetCategories(string sid)
        {
            var response = await Get("categories/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            CategoriesResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<CategoriesResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetCategories \nMessageEx: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                var r =
                    LoggFacade.Error(JsonConvert.SerializeObject(res),
                        "Error on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " +
                        res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            
            var brainCategories = res.result;

            var hierarchy = brainCategories.Where(x => x.parentID != 1 && x.parentID != 0).ToList();
            foreach (var brainCategory in hierarchy)
                brainCategories.Remove(brainCategory);
            var categoriesWithHierarсhy = Recurs(brainCategories, hierarchy);

            return categoriesWithHierarсhy;
        }

        public static List<BrainCategory> Recurs(List<BrainCategory> outCategory, List<BrainCategory> sourceCategory)
        {
            foreach (var categoriesModel in outCategory)
            {
                BrainCategory model = categoriesModel;
                var addCategories = sourceCategory.Where(x => x.parentID == model.categoryID).ToList();
                if(addCategories.Count>0)
                    categoriesModel.Categories.AddRange(addCategories);
                Recurs(categoriesModel.Categories, sourceCategory);
            }
            return outCategory;
        }

        public static async Task<BrainProductsResponse> GetProducts(int categoryId, string sid, int offset = 0)
        {
            String response;
            if (offset > 0)
                response =
                    await
                        Get("products/" + categoryId + "/" + sid + "?offset=" + offset).ConfigureAwait(false);
            else
                response =
                    await
                        Get("products/" + categoryId + "/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();
            BrainProductsResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<BrainProductsResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetProducts \nMessage: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetProducts \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }

        public static async Task<BrainOneProductResponse> GetProduct(int productID, string sid)
        {
            String response = await Get("product/" + productID + "/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();
            BrainOneProductResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<BrainOneProductResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetProduct \nProductID=" + productID + " \nMessage: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                var r =
                    LoggFacade.Error(JsonConvert.SerializeObject(res),
                        "Func: GetProduct \nProductID=" + productID + " \nError on api.brain.com.ua:\nerror_code: " +
                        res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            return res;
        }

        public static async Task<List<Vendor>> GetVendors(string sid)
        {
            var response = await Get("vendors/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();
            VendorsResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<VendorsResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetVendors \nMessageEx: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }

            //var res = JsonConvert.DeserializeObject<VendorsResponse>(response);
            if (res.status != 1)
            {
                var r =
                    LoggFacade.Error(JsonConvert.SerializeObject(res),
                        "Func: GetVendors \nError on api.brain.com.ua:\nerror_code: " + res.error_code +
                        "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            return res.result;
        }

        /// <summary>
        /// Метод для получения времени получения указанного товара на указанных складах
        /// </summary>
        /// <param name="productID">идентификатор товара</param>
        /// <param name="stockID">идентификаторы склада</param>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns>null or DateTime</returns>
        public static async Task<DateTime?> GetDeliveryTime(int productID, int stockID, string sid)
        {
            var response = await Get("delivery_time/" + productID + "/" + stockID + "/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            DeliveryTimeResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<DeliveryTimeResponse>(response);
            }
            catch (Exception ex)
            {
                return null;
            }
            if (res.status != 1)
            {
                //var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetDeliveryTime \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                //XtraMessageBox.Show(ConstV.BrainErrors[res.error_code], "ITFamily.com.ua",
                //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            var longTick = Convert.ToInt64(res.result);
            var dateTime = UnixTimeStampToDateTime(longTick);
            return dateTime;
        }
        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            //1 способ разница в 2часа
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
            //2 способ (на часа меньше)
            //var unixYear0 = new DateTime(1970, 1, 1);
            //long unixTimeStampInTicks = unixTimeStamp * TimeSpan.TicksPerSecond;
            //var dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            //return dtUnix;
        }
        
        /// <summary>
        /// Метод возвращает список валют с названиями ("name"), 
        /// уникальными идентификаторами ("currencyID") и курсами ("value"). 
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static async Task<List<Currencies>> GetCurrencies(string sid)
        {
            var response = await Get("currencies/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            CurrenciesResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<CurrenciesResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetCurrencies \nMessageEx: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetCurrencies \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer);
                return null;
            }
            return res.result;
        }


        /// <summary>
        /// Метод возвращает список контактных лиц с уникальными идентификаторами ("contactID"), 
        /// полными именами ("fullname") и должностями ("position"). 
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns> List of Contacts </returns>
        public static async Task<ContactsResponse> GetContacts(string sid)
        {
            var response = await Get("contacts/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            ContactsResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<ContactsResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetContacts \nMessageEx: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetContacts \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
            }
            return res;
        }

        /// <summary>
        /// Метод для получения комментариев (отзывов) к указанному товару 
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <param name="productID">идентификатор товара</param>
        /// <returns> Метод возвращает список комментариев к указанному товару 
        /// или null в случае ошибки</returns>
        public static async Task<CommentsResponse> GetСomments(string sid, int productID)
        {
            var response = await Get("comments/" + productID + "/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            CommentsResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<CommentsResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetСomments \nProductID=" + productID + "\nMessageEx: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetСomments \nProductID=" + productID + " \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            return res;
        }

        /// <summary>
        /// Метод для получения комментариев (отзывов) к указанному товару 
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <param name="productID">идентификатор товара</param>
        /// <returns> Метод возвращает список комментариев к указанному товару 
        /// или null в случае ошибки</returns>
        public static async Task<FiltersResponse> GetFilters(string sid, int categoryID)
        {
            var response = await Get("filters/" + categoryID + "/" + sid).ConfigureAwait(false);
            if (response == null)
                throw new NoServerResponseException();

            FiltersResponse res;
            try
            {
                res = JsonConvert.DeserializeObject<FiltersResponse>(response);
            }
            catch (Exception ex)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "FuncException: GetСomments \ncategoryID=" + categoryID + "\nMessageEx: " + ex.Message, JsonConvert.SerializeObject(ex)).Result;
                return null;
            }
            if (res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(res), "Func: GetСomments \ncategoryID=" + categoryID + " \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                return null;
            }
            return res;
        }
    }
}