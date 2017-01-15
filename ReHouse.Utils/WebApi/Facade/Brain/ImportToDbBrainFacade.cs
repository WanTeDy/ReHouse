using System;
using System.Threading.Tasks;
using ITfamily.Utils.WebApi.Request.Brain;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade.Brain
{
    public class ImportToDbBrainFacade : BaseFacade
    {
        public static async Task<BaseResponse> ImportToDbBrain(int sleepMiliseconds, DateTime startImport)
        {
            var requestObj = new ImportRequest{DateTime = startImport, Sleep = sleepMiliseconds};
            var response = await Post("api/Import/ImportToDbBrain", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}