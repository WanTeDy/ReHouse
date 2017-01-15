using System.Threading.Tasks;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class ContractorFacade : BaseFacade
    {
        public static async Task<AuthResponse> AddClient(string name, string email, string password, bool check = true)
        {
            var requestObj = new ContractorRequest { Password = password, Name = name, Email = email};
            var response = await Post("api/Clients/AddClient", requestObj, typeof(AuthResponse), check).ConfigureAwait(false);

            var res = response as AuthResponse;
            return res;
        }
        public static async Task<BaseResponse> AddClientFromApp(Contractor contr, string tokenHash)
        {
            var requestObj = new ContractorRequest { Client = contr, TokenHash = tokenHash};
            var response = await Post("api/Clients/AddClientFromApp", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> DeleteClient(int deleteId, string tokenHash, bool check = true)
        {
            var requestObj = new ContractorRequest { SelId = deleteId };
            var response = await Post("api/Clients/DeleteClient", requestObj, typeof(BaseResponse), check).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateClient(Contractor client, string tokenHash, bool check = true)
        {
            var requestObj = new ContractorRequest { Client = client, TokenHash = tokenHash};
            var response = await Post("api/Clients/UpdateClient", requestObj, typeof(BaseResponse), check).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}