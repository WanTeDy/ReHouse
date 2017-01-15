using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITfamily.Utils.DataBaseForLog;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class LoggFacade : BaseFacade
    {
        public static async Task<BaseResponse> Error(string objects, string message, string innerException = "")
        {
            var requestObj = new LogRequest { objects = objects, message = message, innerException = innerException };
            var response = await Post("api/Logg/Error", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<BaseResponse> Error(string objects, string message, State st, string innerException = "")
        {
            var requestObj = new LogRequest { objects = objects, message = message, innerException = innerException, State = st};
            var response = await Post("api/Logg/ErrorWithState", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<BaseResponse> InfoMessage(string message, string objects)
        {
            var requestObj = new LogRequest { objects = objects, message = message };
            var response = await Post("api/Logg/InfoMessageAndObjects", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<BaseResponse> InfoMessage(string message, string objects, string secondObj)
        {
            var requestObj = new LogRequest { objects = objects, message = message, innerException = secondObj };
            var response = await Post("api/Logg/InfoMessageAndSecondObjects", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }

        public static async Task<BaseResponse> InfoState(string message, string objects)
        {
            var requestObj = new LogRequest { objects = objects, message = message };
            var response = await Post("api/Logg/InfoState", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}
