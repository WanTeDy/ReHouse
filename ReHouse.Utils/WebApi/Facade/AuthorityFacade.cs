using System.Collections.Generic;
using System.Threading.Tasks;
using ITfamily.Utils.DataBase.ModelForUI;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.WebApi.Request;
using ITfamily.Utils.WebApi.Response;

namespace ITfamily.Utils.WebApi.Facade
{
    public class AuthorityFacade : BaseFacade
    {
        //ChangeAccessForRole
        //LoadAuthorities
        //LoadRolesWithAuthority
        public static async Task<BaseResponse> ChangeAccessForRole(string tokenHash, int roleId, List<AuthorityForOneRoleModel> authorityForOneRoleModels)
        {
            var requestObj = new AuthorityRequest { TokenHash = tokenHash, RoleId = roleId, AuthorityForOneRoleModels = authorityForOneRoleModels};
            var response = await Post("api/Authority/ChangeAccessForRole", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
        public static async Task<AuthorityResponse> LoadAuthorities(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Authority/LoadAuthorities", requestObj, typeof(AuthorityResponse)).ConfigureAwait(false);

            var res = response as AuthorityResponse;
            return res;
        }
        public static async Task<AuthorityResponse> LoadRolesWithAuthority(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Authority/LoadRolesWithAuthority", requestObj, typeof(AuthorityResponse)).ConfigureAwait(false);

            var res = response as AuthorityResponse;
            return res;
        }

        public static async Task<AuthorityResponse> LoadDataRole(string tokenHash)
        {
            var requestObj = new BaseRequest { TokenHash = tokenHash };
            var response = await Post("api/Authority/LoadDataRole", requestObj, typeof(AuthorityResponse)).ConfigureAwait(false);

            var res = response as AuthorityResponse;
            return res;
        }
        public static async Task<BaseResponse> UpdateProviderRoles(string tokenHash, List<Role> roles)
        {
            var requestObj = new AuthorityRequest { TokenHash = tokenHash, Roles = roles };
            var response = await Post("api/Authority/UpdateProviderRoles", requestObj, typeof(BaseResponse)).ConfigureAwait(false);

            var res = response as BaseResponse;
            return res;
        }
    }
}