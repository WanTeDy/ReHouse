using System.Threading.Tasks;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class FrequencyPaymenFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddStandartFrequencyPayment(string tokenHash)
        {
            var requestObj = new BaseRequest{TokenHash = tokenHash};
            var response = await Post("api/FrequencyPaymen/AddStandartFrequencyPayment", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> AddFrequencyPaymen(string name, string tokenHash)
        {
            var requestObj = new FrequencyRequest { Name = name, TokenHash = tokenHash };
            var response = await Post("api/FrequencyPaymen/AddFrequencyPaymen", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> DeleteFrequencyPaymen(int selId, string tokenHash)
        {
            var requestObj = new FrequencyRequest { SelId = selId, TokenHash = tokenHash };
            var response = await Post("api/FrequencyPaymen/DeleteFrequencyPaymen", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateFrequencyPaymen(string name, int selId, string tokenHash)
        {
            var requestObj = new FrequencyRequest { Name = name, SelId = selId, TokenHash = tokenHash };
            var response = await Post("api/FrequencyPaymen/UpdateFrequencyPaymen", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}