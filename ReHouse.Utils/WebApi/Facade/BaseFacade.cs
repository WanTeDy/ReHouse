using System;
using System.Net.Http;
using System.Threading.Tasks;
using ITfamily.Utils.Except;
using ITfamily.Utils.Except.ApiException;
using ITfamily.Utils.Helpers;
using ITfamily.Utils.Logging;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class BaseFacade
    {
        protected static String UriPath = "http://localhost:2514/";//"http://www.itfamily.com.ua/webapi/";
        protected static async Task<Object> Post(String path, BaseRequest request, Type responseType, bool check = true)
        {
            if (check)
            {
                //if (Internet.CheckConnection())
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri(UriPath);
                        //"http://itfamily.azurewebsites.net/");

                        Object resp;
                        try
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(360);
                            var response = await httpClient.PostAsJsonAsync(path, request).ConfigureAwait(false);
                            if (!response.IsSuccessStatusCode) return null;
                            resp = await response.Content.ReadAsAsync(responseType).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            UiHelper.Error("Сервер не отвечает. Ошибка при отправке запроса!");
                                //throw new Exception("Сервер не отвечает. Ошибка при отправке запроса!");
                            return null;
                        }
                        CheckErrors(resp);
                        return resp;
                    }
                }
                //else
                //{
                //    UiHelper.Error("Интернет соединение пропало. Восстановите соединение");
                //    return null;
                //}
            }
            else
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(UriPath);

                    Object resp;
                    try
                    {
                        httpClient.Timeout = TimeSpan.FromSeconds(360);
                        var response = await httpClient.PostAsJsonAsync(path, request).ConfigureAwait(false);
                        if (!response.IsSuccessStatusCode) return null;
                        resp = await response.Content.ReadAsAsync(responseType).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    return resp;
                }
            }
        }

        protected static void CheckErrors(Object resp)
        {
            AppLog.Handler(() =>
            {
                var check = resp as BaseResponse;
                if (check == null) throw new NoServerResponseException("Нет ответа от сервера!");
                if (check.ErrorCode == (Int32)ErrorCodes.Success)
                    return;
                throw new ItFamilyException(check.ExceptionMessage);
            });
        }
    }
}