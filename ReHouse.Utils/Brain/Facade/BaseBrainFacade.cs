using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using ITfamily.Utils.Brain.Request;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils.Brain.Facade
{
    /// <summary>
    ///     Base class for all facades
    /// </summary>
    public abstract class BaseBrainFacade
    {
        protected static async Task<Object> Post(String path, BaseBrainRequest request, Type responseType)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://api.brain.com.ua/");
                HttpContent val = new StringContent(request.ToQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(path, val).ConfigureAwait(false);
                //var response = await httpClient.PostAsJsonAsync(path, val).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) return null;
                var resp = await response.Content.ReadAsAsync(responseType).ConfigureAwait(false);
                //CheckForErrors(resp);
                return resp;
            }
        }
        protected static async Task<Object> Post2(String path, String request, Type responseType)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://api.brain.com.ua/");
                //HttpContent val = new StringContent(request.ToQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.PostAsync(path, request, new JsonMediaTypeFormatter()).ConfigureAwait(false);
                //var response = await httpClient.PostAsJsonAsync(path, val).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) return null;
                var resp = await response.Content.ReadAsAsync(responseType).ConfigureAwait(false);
                //CheckForErrors(resp);
                return resp;
            }
        }
        protected static async Task<Object> Post3(String path, BaseBrainRequest request, Type responseType)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://api.brain.com.ua/");
                HttpContent val = new StringContent(request.ToQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                //var response = await httpClient.PostAsync(path, val).ConfigureAwait(false);
                var response = await httpClient.PostAsJsonAsync(path, val).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) return null;
                var resp = await response.Content.ReadAsAsync(responseType).ConfigureAwait(false);
                //CheckForErrors(resp);
                return resp;
            }
        }
        protected static async Task<String> Get(String path)
        {

            //var request1 = (HttpWebRequest)WebRequest.Create("http://api.brain.com.ua/" + path);

            //var response = (HttpWebResponse)request1.GetResponse();

            //if (response != null)
            //{
            //    var stream = response.GetResponseStream();
            //    if (stream != null)
            //    {
            //        var responseString = new StreamReader(stream).ReadToEnd();
            //        return responseString;
            //    }
            //}
            //return null;
            using (var client = new WebClient())
            {
                var responseString = client.DownloadString(new Uri(@"http://api.brain.com.ua/" + path));//.ConfigureAwait(false);
                return responseString;
            }
            //using (var client = new HttpClient())
            //{
            //    var responseString = await client.GetStringAsync(new Uri(@"http://api.brain.com.ua/" + path)).ConfigureAwait(false);
            //    return responseString;
            //}
        }

       /* protected static async Task<String> Get2(String path)
        {            
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://api.brain.com.ua/");
                //HttpContent val = new StringContent(request.ToQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.GetAsync(path).ConfigureAwait(false);//httpClient.PostAsync(path, val).ConfigureAwait(false);
                //var response = await httpClient.PostAsJsonAsync(path, val).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) return null;
                var resp = await response.Content.ReadAsStringAsync();
                //CheckForErrors(resp);
                return resp;
            }
        }*/

        /// <summary>
        ///     Throw exception if error code presents
        /// </summary>
        /// <param name="resp"></param>
        //protected static void CheckForErrors(Object resp)
        //{
        //    var check = resp as BaseResponse;
        //    if (check == null) throw new ErrorConverToBaseResponse();
        //    if (check.ErrorCode == (Int32)ErrorCodes.Success)
        //        return;
        //    if (check.ErrorCode == (Int32)ErrorCodes.UnhandledErrorCode)
        //        throw new BaseException();
        //    if (check.ErrorCode != (Int32)ErrorCodes.NoErrorCode)
        //        throw ExceptionHandler.Dict[check.ErrorCode];
        //}
    }
}