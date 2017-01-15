using System;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.AuxiliaryData;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class AuthFacade : BaseFacade
    {
        public static async Task<BaseResponse> AddAdminEmployerIfDbIsEmpty()
        {
            var requestObj = new BaseRequest();
            var response = await Post("api/Auth/AddAdminEmployerIfDbIsEmpty", requestObj, typeof(BaseResponse)).ConfigureAwait(false);
            var res = response as BaseResponse;
            return res;
        }
        public static async Task<BaseResponse> AddStandartsRoles()
        {
            var requestObj = new BaseRequest();
            var response = await Post("api/Auth/AddStandartsRoles", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<AuthResponse> CheckSignInData(String login, String password, bool check = true)
        {
            var requestObj = new EmployersRequest { Login = login, Password = password };
            var response = await Post("api/Auth/CheckSignInData", requestObj, typeof(AuthResponse), check).ConfigureAwait(false);

            var res = response as AuthResponse;
            return res;
        }
        public static async Task<AuthResponse> ExternalSignIn(String providerKey, Provider provider, String googleEmail = "", bool check = false)
        {
            var requestObj = new EmployersRequest { ProviderKey = providerKey, Provider = provider, GoogleEmail = googleEmail};
            var response = await Post("api/Auth/ExternalSignIn", requestObj, typeof(AuthResponse), check).ConfigureAwait(false);

            var res = response as AuthResponse;
            return res;
        }

        public static async Task<RemindResponse> RecoveryPassword(String email, bool check = false)
        {
            var requestObj = new RemindRequest { Email = email};
            var response = await Post("api/Auth/RecoveryPassword", requestObj, typeof(RemindResponse), check).ConfigureAwait(false);

            var res = response as RemindResponse;
            return res;
        }

        //CheckTokenForAccess
        public static async Task<RemindResponse> CheckTokenForAccess(String token, bool check = false)
        {
            var requestObj = new RemindRequest { TokenHash = token };
            var response = await Post("api/Auth/CheckTokenForAccess", requestObj, typeof(RemindResponse), check).ConfigureAwait(false);

            var res = response as RemindResponse;
            return res;
        }
        public static async Task<AuthResponse> SetPassword(String token, string password, bool check = false)
        {
            var requestObj = new RemindRequest { TokenHash = token, Password = password };
            var response = await Post("api/Auth/SetPassword", requestObj, typeof(AuthResponse), check).ConfigureAwait(false);

            var res = response as AuthResponse;
            return res;
        }
    }
}