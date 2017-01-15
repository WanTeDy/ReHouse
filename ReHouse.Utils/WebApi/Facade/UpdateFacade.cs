using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.OtherOurDataForDb;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class UpdateFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddOrUpdateFile(byte[] bytes, string nameFile, string urlPath, byte[] hashMd5, string tokenHash)
        {
            var requestObj = new UpdateRequest { TokenHash = tokenHash, NameFile = nameFile, HashMD5 = hashMd5, Bytes = bytes, UrlPath = urlPath };
            var response = await Post("api/Update/AddOrUpdateFile", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<UpdateResponse> CheckForUpdateFile(string nameFile, string urlPath, byte[] hashMd5, string tokenHash)
        {
            var requestObj = new UpdateRequest { TokenHash = tokenHash, NameFile = nameFile, HashMD5 = hashMd5, UrlPath = urlPath };
            var response = await Post("api/Update/CheckForUpdateFile", requestObj, typeof(UpdateResponse)).ConfigureAwait(false);

            var res = response as UpdateResponse;
            return res;
        }
        public static async Task<BaseResponse> CheckAfterUpdate(List<UpdateFile> updateFiles, string tokenHash)
        {
            var requestObj = new UpdateRequest { TokenHash = tokenHash, UpdateFiles = updateFiles};
            var response = await Post("api/Update/CheckAfterUpdate", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<UpdateResponse> GetAuxiliaryFilesData(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Update/GetAuxiliaryFilesData", requestObj, typeof(UpdateResponse)).ConfigureAwait(false);

            var res = response as UpdateResponse;
            return res;
        }
    }
}