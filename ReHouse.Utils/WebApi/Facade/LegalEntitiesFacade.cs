using System.Threading.Tasks;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class LegalEntitiesFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddLegalEntity(Contractor legalEntity, string tokenHash, bool check = true)
        {
            var requestObj = new LegalEntityRequest { LegalEntity = legalEntity, TokenHash = tokenHash };
            var response = await Post("api/LegalEntities/AddLegalEntity", requestObj, typeof(BaseResponse), check).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> DeleteLegalEntity(int deleteId, string tokenHash)
        {
            var requestObj = new LegalEntityRequest { SelId = deleteId, TokenHash = tokenHash };
            var response = await Post("api/LegalEntities/DeleteLegalEntity", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateLegalEntity(Contractor legalEntity, string tokenHash)
        {
            var requestObj = new LegalEntityRequest { LegalEntity = legalEntity, TokenHash = tokenHash };
            var response = await Post("api/LegalEntities/UpdateLegalEntity", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}