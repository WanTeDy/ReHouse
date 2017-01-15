using System.Threading.Tasks;
using ITfamily.Utils.DataBase;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class ServiceFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddService(string tokenHash, Services serv)
        {
            var requestObj = new ServiceRequest { TokenHash = tokenHash, Service = serv };
            var response = await Post("api/Service/AddService", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> DeleteService(string tokenHash, int selId)
        {
            var requestObj = new ServiceRequest { TokenHash = tokenHash, SelId = selId };
            var response = await Post("api/Service/DeleteService", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateService(string tokenHash, Services serv)
        {
            var requestObj = new ServiceRequest { TokenHash = tokenHash, Service = serv };
            var response = await Post("api/Service/UpdateService", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}