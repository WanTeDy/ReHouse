using System;
using System.Threading.Tasks;
using ITfamily.Utils.Brain.Request;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.Except.ApiException;
using ITfamily.Utils.WebApi.Facade;
using Newtonsoft.Json;

namespace ITfamily.Utils.Brain.Facade
{
    public class AuthBrainFacade : BaseBrainFacade
    {
        /// <summary>
        /// Метод для получения идентификатора сессии 
        /// </summary>
        /// <param name="login">логин пользователя дилерского портала с правами руководителя (администратора)</param>
        /// <param name="password">пароль пользователя захешированный MD5 функцией</param>
        /// <returns>Метод возвращает идентификатор сессии (SID), который необходим для реализации последующих запросов</returns>
        public static async Task<BaseBrainResponse> Auth(string login, string password)
        {
            try
            {
                var requestObj = new BrainAuthRequest { login = login, password = password };
                var response = await Post("auth", requestObj, typeof(BaseBrainResponse)).ConfigureAwait(false);

                var res = response as BaseBrainResponse;
                if (res == null)
                {
                    var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "Func: Auth \nLogin=" + login + " \nPassword:" + password + " \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                    throw new NoServerResponseException();
                }
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод для ликвидации текущей сессии 
        /// </summary>
        /// <param name="sid">идентификатор сессии</param>
        /// <returns>Происходит прерывание текущей сессии </returns>
        public static async Task<BaseBrainResponse> LogOut(string sid)
        {
            var requestObj = new BrainLogoutRequest { SID = sid};
            var response = await Post("logout", requestObj, typeof(BaseBrainResponse)).ConfigureAwait(false);

            var res = response as BaseBrainResponse;
            if (res == null || res.status != 1)
            {
                var r = LoggFacade.Error(JsonConvert.SerializeObject(response), "Func: LogOut \nError on api.brain.com.ua:\nerror_code: " + res.error_code + "\nerror_message: " + res.error_message, State.ErrorOnBrainApiServer).Result;
                throw new NoServerResponseException();
            }
            return res;
        }
    }
}